using Dormitories.Contracts.Dormitories.GetRoomById;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RedLockNet.SERedis;
using Reservations.Reservations.Models;
using Reservations.Reservations.Services;
using Reservations.Reservations.ValueObjects;
using Shared.Contracts.CQRS;
using Shared.Exceptions;

namespace Reservations.Reservations.Features.CreateReservation.Handler;

internal class CreateReservationHandler(
    ISender sender,
    ILogger<CreateReservationHandler> logger,
    ReservationDbContext reservationDbContext,
    IDistributedLockService distributedLockService,
    IRedisService redisService,
    IReservationService reservationService)
    : ICommandHandler<CreateReservationCommand, CreateReservationResult>
{
    public async Task<CreateReservationResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        await reservationService.ValidateUserReservationAsync(request.UserId, cancellationToken);
        var roomId = request.RoomId;
        var roomResourceKey = $"room-reservation-{roomId}";

        await using var redLock = await distributedLockService.AcquireLockAsync(roomResourceKey, cancellationToken);
        if (!redLock.IsAcquired)
        {
            throw new Exception("Failed to acquire lock. Room is currently being reserved by another user.");
        }

        using var transaction = await reservationDbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var capacity = await redisService.GetOrSetRoomCapacityAsync(roomId, cancellationToken);
            var currentOccupants = await redisService.IncrementOccupantsAsync(roomId, capacity, cancellationToken);

            var roomToReserve = await sender.Send(new GetRoomByIdQuery(roomId), cancellationToken);

            var reservation = Reservation.Create(
                Guid.NewGuid(),
                roomId,
                request.UserId,
                RoomInfo.Of(roomToReserve.RoomDto.Number, roomToReserve.RoomDto.Price, roomToReserve.RoomDto.Capacity));

            await reservationService.CreateReservationAsync(reservation, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            logger.LogInformation("Reservation for room {roomId} successfully created.", roomId);

            return new CreateReservationResult();
        }
        catch (Exception ex)
        {
            await redisService.DecrementOccupantsAsync(roomId, cancellationToken);
            await transaction.RollbackAsync(cancellationToken);
            logger.LogError(ex, "Error while creating reservation for room {roomId}", roomId);
            throw;
        }
    }
}
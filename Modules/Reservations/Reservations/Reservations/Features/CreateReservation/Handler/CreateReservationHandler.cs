using Dormitories.Contracts.Dormitories.GetRoomById;
using Microsoft.Extensions.Logging;
using Reservations.Reservations.Services;
using Reservations.Reservations.ValueObjects;
using Shared.Contracts.CQRS;

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
    public async Task<CreateReservationResult> Handle(CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var semester = await reservationDbContext.Semesters.FirstOrDefaultAsync(r => r.Name == request.SemesterName,
            cancellationToken: cancellationToken);

        if (semester == null) throw new NotFoundException("Semester not found to reserve");
        // maybe to remove Logic is in DDD model Semester
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

            semester.AddReservation(request.UserId, request.RoomId,
                RoomInfo.Of(roomToReserve.RoomDto.Number, roomToReserve.RoomDto.Price, roomToReserve.RoomDto.Capacity));

            //await reservationService.CreateReservationAsync(reservation, cancellationToken);
            await reservationDbContext.SaveChangesAsync(cancellationToken);
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
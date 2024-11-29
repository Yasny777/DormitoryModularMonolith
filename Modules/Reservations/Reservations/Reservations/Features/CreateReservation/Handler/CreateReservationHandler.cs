using Dormitories.Contracts.Dormitories.GetRoomById;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using RedLockNet.SERedis;
using Reservations.Reservations.Models;
using Shared.Contracts.CQRS;
using Shared.Exceptions;

namespace Reservations.Reservations.Features.CreateReservation.Handler;

internal class CreateReservationHandler(
    ISender sender,
    ReservationDbContext reservationDbContext,
    RedLockFactory redLockFactory,
    IDistributedCache redis)
    : ICommandHandler<CreateReservationCommand, CreateReservationResult>
{
    public async Task<CreateReservationResult> Handle(CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        // check if user has already reservation
        var reservationActive =
            await reservationDbContext.Reservations.SingleOrDefaultAsync(r => r.UserId == request.UserId,
                cancellationToken);
        if (reservationActive != null) throw new BadRequestException("User already has active reservation");

        var roomId = request.RoomId;
        var roomResourceKey = $"room-reservation-{roomId}";
        var redisKeyOccupants = $"room:{roomId}:occupants";
        var redisKeyCapacity = $"room:{roomId}:capacity";
        var lockExpiry = TimeSpan.FromSeconds(10);
        var waitTime = TimeSpan.FromSeconds(2);
        var retryTime = TimeSpan.FromMilliseconds(500);

        await using (var redLock =
                     await redLockFactory.CreateLockAsync(roomResourceKey, lockExpiry, waitTime, retryTime))
        {
            if (redLock.IsAcquired)
            {
                var incrementedFlag = false;
                try
                {
                    var capacity = await redis.GetStringAsync(redisKeyCapacity, cancellationToken);
                    var currentOccupants = await redis.GetStringAsync(redisKeyOccupants, cancellationToken);
                    if (capacity.IsNullOrEmpty())
                    {
                        var room = await sender.Send(new GetRoomByIdQuery(roomId), cancellationToken);
                        await redis.SetStringAsync(redisKeyCapacity, room.RoomDto.Capacity.ToString(),
                            cancellationToken);
                        capacity = room.RoomDto.Capacity.ToString();
                    }

                    //await Task.Delay(5000);
                    if (currentOccupants.IsNullOrEmpty())
                    {
                        await redis.SetStringAsync(redisKeyOccupants, "1", cancellationToken);
                        incrementedFlag = true;
                    }
                    else
                    {
                        var occupantsCount = int.Parse(currentOccupants);
                        var capacityCount = int.Parse(capacity);
                        if (occupantsCount >= capacityCount)
                        {
                            throw new Exception("Room is fully booked.");
                        }

                        occupantsCount += 1;
                        incrementedFlag = true;

                        await redis.SetStringAsync(redisKeyOccupants, occupantsCount.ToString(), cancellationToken);
                    }

                    // Create reservation
                    var reservation = Reservation.Create(Guid.NewGuid(), roomId, request.UserId);
                    await reservationDbContext.Reservations.AddAsync(reservation, cancellationToken);
                    await reservationDbContext.SaveChangesAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    if (incrementedFlag != true) throw;

                    var currentOccupants = await redis.GetStringAsync(redisKeyOccupants, cancellationToken);
                    var occupantsCount = int.Parse(currentOccupants);
                    occupantsCount -= 1;
                    await redis.SetStringAsync(redisKeyOccupants, occupantsCount.ToString(), cancellationToken);
                }
                finally
                {
                    // zwalnia blokade za pomoca IDisposable
                }
            }
            else
            {
                throw new Exception("Failed to acquire lock. Other User try to reserve this room. Try again later.");
            }
        }


        return new CreateReservationResult();
    }
}
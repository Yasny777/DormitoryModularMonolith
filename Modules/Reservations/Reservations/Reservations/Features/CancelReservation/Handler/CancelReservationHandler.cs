namespace Reservations.Reservations.Features.CancelReservation.Handler;

internal class CancelReservationHandler(ReservationDbContext reservationDbContext, IRedisService redisService)
    : ICommandHandler<CancelReservationCommand, CancelReservationResult>
{
    public async Task<CancelReservationResult> Handle(CancelReservationCommand request,
        CancellationToken cancellationToken)
    {
        using var transaction = await reservationDbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var reservation =
                await reservationDbContext
                    .Reservations
                    .Include(reservation => reservation.Semester)
                    .FirstOrDefaultAsync(r => r.Id == request.ReservationId,
                        cancellationToken);

            if (reservation == null) throw new NotFoundException("Reservation", request.ReservationId);

            var semester = reservation.Semester;
            semester.CancelReservation(reservation.Id);

            await reservationDbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            await redisService.DecrementOccupantsAsync(reservation.RoomId, cancellationToken);

            return new CancelReservationResult(true);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
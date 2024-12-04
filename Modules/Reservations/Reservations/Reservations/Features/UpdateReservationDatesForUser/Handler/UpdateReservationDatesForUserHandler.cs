using Microsoft.EntityFrameworkCore;
using Reservations.Reservations.Features.UpdateReservationDatesForUser.Endpoint;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.UpdateReservationDatesForUser.Handler;

internal class UpdateReservationDatesForUserHandler(ReservationDbContext dbContext)
    : ICommandHandler<UpdateReservationDatesForUserCommand, UpdateReservationDatesForUserResult>
{
    public async Task<UpdateReservationDatesForUserResult> Handle(UpdateReservationDatesForUserCommand command, CancellationToken cancellationToken)
    {
        var reservation = await dbContext.Reservations
            .FirstOrDefaultAsync(r => r.Id == command.ReservationId, cancellationToken);

        if (reservation == null)
            throw new NotFoundException($"Reservation with ID {command.ReservationId} not found.");

        // Update the reservation
        reservation.UpdateDates(command.NewStartDate, command.NewEndDate);

        dbContext.Reservations.Update(reservation);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateReservationDatesForUserResult(reservation.Id, reservation.StartDate, reservation.EndDate);
    }
}
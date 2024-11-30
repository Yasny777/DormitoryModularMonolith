using Reservations.Reservations.Models;
using Reservations.Reservations.ValueObjects;

namespace Reservations.Reservations.Services;

public interface IReservationService
{
    Task ValidateUserReservationAsync(Guid userId, CancellationToken cancellationToken);
    Task CreateReservationAsync(Reservation reservation, CancellationToken cancellationToken);
}

public class ReservationService : IReservationService
{
    private readonly ReservationDbContext _dbContext;

    public ReservationService(ReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateUserReservationAsync(Guid userId, CancellationToken cancellationToken)
    {
        var reservationActive = await _dbContext.Reservations
            .Where(r => r.Status == ReservationStatus.Active)
            .SingleOrDefaultAsync(r => r.UserId == userId, cancellationToken);

        if (reservationActive != null)
        {
            throw new BadRequestException("User already has active reservation");
        }
    }

    public async Task CreateReservationAsync(Reservation reservation, CancellationToken cancellationToken)
    {
        await _dbContext.Reservations.AddAsync(reservation, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

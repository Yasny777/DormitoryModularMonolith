namespace Reservations.Reservations.Models;

public class Semester : Aggregate<Guid>
{
    public string Name { get; private set; } = default!;
    public int Number { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<Reservation> _reservations = [];
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public PriorityWindow? PriorityWindow { get; private set; }

    public static Semester Create(Guid id, string name, int number, DateTime startDate, DateTime endDate, bool isActive)
    {
        var semester = new Semester()
        {
            Id = id,
            Name = name,
            Number = number,
            StartDate = startDate.ToUniversalTime(),
            EndDate = endDate.ToUniversalTime(),
            IsActive = isActive
        };

        return semester;
    }

    public void SetPriorityWindow(List<string> roleNames, DateTime startDateTime, DateTime endDateTime)
    {
        if (roleNames == null || !roleNames.Any())
            throw new ArgumentException("At least one role must be specified.");

        PriorityWindow = new PriorityWindow(Id, this, roleNames, startDateTime, endDateTime);
    }

    public void AddReservation(Guid userId, Guid roomId, RoomInfo roomInfo, string userRole)
    {
        var reservationActive =
            _reservations.FirstOrDefault(r => r.UserId == userId && r.Status == ReservationStatus.Active);

        if (reservationActive != null) throw new BadRequestException("User Already has reservation!");

        if (PriorityWindow != null)
        {
            var now = DateTime.UtcNow;

            if (PriorityWindow.IsWithinWindow(now) && !PriorityWindow.IsRoleAllowed(userRole))
            {
                throw new UnauthorizedAccessException(
                    "Brak uprawnień do rezerwacji priorytetowej");
            }
        }

        var reservation = new Reservation(roomId, userId, this.StartDate, this.EndDate, ReservationStatus.Active,
            roomInfo, this.Id, this);

        _reservations.Add(reservation);

        AddDomainEvent(new ReservationCreatedEvent(reservation));
    }

    public void CancelReservation(Guid reservationId)
    {
        var reservation = _reservations.FirstOrDefault(r => r.Id == reservationId);
        if (reservation == null) throw new NotFoundException("Reservation not found");

        if (reservation.Status == ReservationStatus.Cancelled)
            throw new BadRequestException("Reservation already cancelled");

        reservation.CancelReservation();

        // Emit Domain Event
        AddDomainEvent(new ReservationCancelledEvent(reservation));
    }
}
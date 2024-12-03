using Reservations.Reservations.Events;
using Reservations.Reservations.ValueObjects;
using Shared.DDD;

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

    public static Semester Create(Guid id, string name, int number, DateTime startDate, DateTime endDate, bool isActive)
    {
        var semester = new Semester()
        {
            Id = id,
            Name = name,
            Number = number,
            StartDate = startDate.ToUniversalTime(),
            EndDate = endDate.ToUniversalTime(),
            IsActive = isActive,
        };

        return semester;
    }

    public void AddReservation(Guid userId, Guid roomId, RoomInfo roomInfo)
    {
        var reservationActive = _reservations.FirstOrDefault(r => r.UserId == userId);

        // sprawdza czy User ma rezerwacje
        if (reservationActive != null) throw new BadRequestException("User Already has reservation!");

        // startdate i enddate jest przypisany do semestru, chyba ze admin zmieni dla usera
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
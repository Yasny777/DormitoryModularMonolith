﻿namespace Reservations.Reservations.Models;

public class Reservation : Entity<Guid>
{
    public Guid RoomId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public ReservationStatus Status { get; private set; } = default!;

    public RoomInfo RoomInfo { get; private set; } = default!;

    public Guid SemesterId { get; private set; }

    public Semester Semester { get; private set; } = default!;

    private Reservation() { }
    internal Reservation(Guid roomId, Guid userId, DateTime startDate, DateTime endDate, ReservationStatus status, RoomInfo roomInfo, Guid semesterId, Semester semester)
    {
        RoomId = roomId;
        UserId = userId;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        RoomInfo = roomInfo;
        SemesterId = semesterId;
        Semester = semester;
    }

    public void UpdateDates(DateTime newStartDate, DateTime newEndDate)
    {
        if (newStartDate >= newEndDate)
            throw new ArgumentException("Start date must be before end date.");

        StartDate = newStartDate.ToUniversalTime();
        EndDate = newEndDate.ToUniversalTime();
    }

    public void Update(string number, int capacity, decimal price)
    {
        RoomInfo = RoomInfo.Of(number, price, capacity);
    }

    public void CancelReservation()
    {
        Status = ReservationStatus.Cancelled;
    }
}
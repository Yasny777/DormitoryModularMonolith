using MediatR;

namespace Shared.Events;

//todo   integration events with both modules
//todo   to cancel reservations for all users who reserved this room in Reservation Module
//todo   and remove reference to room from Identity Module
public record RoomRemovedIntegrationEvent(Guid RoomId) : INotification
{
}
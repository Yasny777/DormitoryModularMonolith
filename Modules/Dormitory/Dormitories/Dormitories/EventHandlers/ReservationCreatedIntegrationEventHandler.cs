using Shared.Contracts.CQRS;
using Shared.Events;

namespace Dormitories.Dormitories.EventHandlers;

public class ReservationCreatedIntegrationEventHandler : INotificationHandler<ReservationCreatedIntegrationEvent>
{
    public async Task Handle(ReservationCreatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        // todo integration

    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.DDD;

namespace Shared.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    private readonly Dictionary<DbContext, List<IDomainEvent>> _domainEvents = new();

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        CollectDomainEvents(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        await CollectDomainEventsAsync(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEventsAsync(eventData.Context, cancellationToken);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        ClearDomainEvents(eventData.Context);
        base.SaveChangesFailed(eventData);
    }

    public override async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        ClearDomainEvents(eventData.Context);
        await base.SaveChangesFailedAsync(eventData, cancellationToken);
    }

    private void CollectDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        // Collect and clear events
        var domainEvents = aggregates.SelectMany(a => a.DomainEvents).ToList();
        aggregates.ToList().ForEach(a => a.ClearDomainEvents());

        if (!_domainEvents.ContainsKey(context))
            _domainEvents[context] = new List<IDomainEvent>();

        _domainEvents[context].AddRange(domainEvents);
    }

    private async Task CollectDomainEventsAsync(DbContext? context)
    {
        if (context == null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        // Collect and clear events
        var domainEvents = aggregates.SelectMany(a => a.DomainEvents).ToList();
        aggregates.ToList().ForEach(a => a.ClearDomainEvents());

        if (!_domainEvents.ContainsKey(context))
            _domainEvents[context] = new List<IDomainEvent>();

        _domainEvents[context].AddRange(domainEvents);
    }

    private async Task DispatchDomainEventsAsync(DbContext? context, CancellationToken cancellationToken)
    {
        if (context == null || !_domainEvents.ContainsKey(context)) return;

        var domainEvents = _domainEvents[context];
        _domainEvents.Remove(context);

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }
    }

    private void ClearDomainEvents(DbContext? context)
    {
        if (context != null && _domainEvents.ContainsKey(context))
        {
            _domainEvents.Remove(context);
        }
    }
}

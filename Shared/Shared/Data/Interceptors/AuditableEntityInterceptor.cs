﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.DDD;

namespace Shared.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? dbContext)
    {
        if (dbContext == null) return;

        foreach (var entry in dbContext.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = ""; // todo retrieve from token need to inject httpcontextaccessor
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry.State is EntityState.Added or EntityState.Modified
                || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = ""; // todo retrieve from token
                entry.Entity.LastModified = DateTime.UtcNow;
            }
        }
    }
}


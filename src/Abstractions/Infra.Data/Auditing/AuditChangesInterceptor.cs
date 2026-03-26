using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Goal.Infra.Data.Auditing;

public abstract class AuditChangesInterceptor : SaveChangesInterceptor
{
    private bool _isSaving;

    public abstract string GetCurrentUserId();

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (_isSaving || eventData.Context is null)
            return base.SavingChanges(eventData, result);

        try
        {
            _isSaving = true;

            SaveAudits(
                eventData.Context,
                CollectAudits(eventData.Context));

            return base.SavingChanges(eventData, result);
        }
        finally
        {
            _isSaving = false;
        }
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (_isSaving || eventData.Context is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        try
        {
            _isSaving = true;

            await SaveAuditsAsync(
                eventData.Context,
                CollectAudits(eventData.Context),
                cancellationToken);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        finally
        {
            _isSaving = false;
        }
    }

    private List<AuditLog> CollectAudits(DbContext context)
    {
        var audits = new List<AuditLog>();

        foreach (EntityEntry entry in context.ChangeTracker.Entries())
        {
            if (ShouldAudit(entry))
                audits.Add(CreateAuditEntry(entry));
        }

        return audits;
    }

    private static void SaveAudits(DbContext context, List<AuditLog> audits)
    {
        if (audits.Count == 0)
            return;

        context.Set<AuditLog>().AddRange(audits);
        context.SaveChanges();
    }

    private static async Task SaveAuditsAsync(DbContext context, List<AuditLog> audits, CancellationToken cancellationToken = default)
    {
        if (audits.Count == 0)
            return;

        await context.Set<AuditLog>().AddRangeAsync(audits, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private AuditLog CreateAuditEntry(EntityEntry entry)
    {
        string tableName = entry.Metadata.GetTableName()!;
        string? schema = entry.Metadata.GetSchema();
        var identifier = StoreObjectIdentifier.Table(tableName, schema);

        var audit = new AuditLog()
        {
            EntityName = tableName,
            ChangeType = entry.State.ToString(),
            ChangedBy = GetCurrentUserId(),
            Timestamp = DateTime.UtcNow,
            KeyValues = JsonSerializer.Serialize(
                entry.Properties
                    .Where(p => p.Metadata.IsPrimaryKey())
                    .ToDictionary(p => p.Metadata.GetColumnName(identifier)!, p => p.CurrentValue))
        };

        if (entry.State == EntityState.Added)
        {
            audit.NewValues = JsonSerializer.Serialize(entry.CurrentValues.ToObject());
            return audit;
        }

        if (entry.State == EntityState.Deleted)
        {
            audit.OldValues = JsonSerializer.Serialize(entry.OriginalValues.ToObject());
            return audit;
        }

        var changes = entry.Properties
            .Where(p => p.IsModified && !Equals(p.OriginalValue, p.CurrentValue))
            .ToDictionary(
                p => p.Metadata.GetColumnName(identifier)!,
                p => new { Old = p.OriginalValue, New = p.CurrentValue }
            );

        audit.OldValues = changes.Count != 0
            ? JsonSerializer.Serialize(changes.ToDictionary(k => k.Key, k => k.Value.Old))
            : null;

        audit.NewValues = changes.Count != 0
            ? JsonSerializer.Serialize(changes.ToDictionary(k => k.Key, k => k.Value.New))
            : null;

        return audit;
    }

    private static bool ShouldAudit(EntityEntry entry)
    {
        return entry.Entity is not AuditLog
            && (entry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);
    }
}

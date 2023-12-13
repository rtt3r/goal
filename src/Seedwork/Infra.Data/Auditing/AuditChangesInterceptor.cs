using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Goal.Seedwork.Infra.Data.Auditing;

public abstract class AuditChangesInterceptor : SaveChangesInterceptor, IAuditChangesInterceptor
{
    protected Audit? _audit;

    public event EventHandler<SaveAuditEventArgs>? SaveAudit;

    protected AuditChangesInterceptor()
    {
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
        => await Task.Run(() => SavingChanges(eventData, result), cancellationToken).ConfigureAwait(false);

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        _audit = CreateAudit(eventData.Context);
        return result;
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = new CancellationToken())
        => await Task.Run(() => SavedChanges(eventData, result), cancellationToken).ConfigureAwait(false);

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (_audit is null)
            return result;

        _audit.Succeeded = true;
        _audit.EndTime = DateTime.UtcNow;

        SaveAuditChangesInternal(_audit);

        return result;
    }

    public override async Task SaveChangesFailedAsync(
        DbContextErrorEventData eventData,
        CancellationToken cancellationToken = new CancellationToken())
        => await Task.Run(() => SaveChangesFailed(eventData), cancellationToken).ConfigureAwait(false);

    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        if (_audit is null)
            return;

        _audit.Succeeded = false;
        _audit.EndTime = DateTime.UtcNow;
        _audit.ErrorMessage = eventData.Exception.Message;

        SaveAuditChangesInternal(_audit);
    }

    protected abstract void SaveAuditChanges(Audit audit);

    private static Audit? CreateAudit(DbContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);

        context?.ChangeTracker.DetectChanges();
        var audit = new Audit { StartTime = DateTimeOffset.UtcNow };

        foreach (EntityEntry entry in context!.ChangeTracker
            .Entries()
            .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted))
        {
            audit.Entries.Add(CreateAuditEntry(entry));
        }

        return audit;
    }

    private static AuditEntry CreateAuditEntry(EntityEntry entry)
    {
        string tableName = entry.Metadata.GetTableName()
            ?? throw new InvalidOperationException("Cannot obtain the table name");

        string? schema = entry.Metadata.GetSchema();

        var keyValues = new Dictionary<string, object?>();
        var oldValues = new Dictionary<string, object?>();
        var newValues = new Dictionary<string, object?>();
        var changedColumns = new List<string>();
        AuditType auditType = AuditType.None;

        var identifier = StoreObjectIdentifier.Table(tableName, schema);

        string propertyName;
        string? columnName;

        foreach (PropertyEntry property in entry.Properties)
        {
            propertyName = property.Metadata.Name;
            columnName = property.Metadata.GetColumnName(identifier)
                ?? throw new InvalidOperationException("Cannot obtain the column name");

            if (property.Metadata.IsPrimaryKey())
            {
                keyValues[propertyName] = property.CurrentValue;
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    newValues[propertyName] = property.CurrentValue;
                    auditType = AuditType.Create;
                    break;

                case EntityState.Deleted:
                    oldValues[propertyName] = property.OriginalValue;
                    auditType = AuditType.Delete;
                    break;

                case EntityState.Modified when property.IsModified
                    && property.OriginalValue is not null
                    && !property.OriginalValue.Equals(property.CurrentValue):

                    changedColumns.Add(columnName);

                    oldValues[propertyName] = property.OriginalValue;
                    newValues[propertyName] = property.CurrentValue;
                    auditType = AuditType.Update;
                    break;
            }
        }

        return new AuditEntry
        {
            AuditType = auditType.ToString(),
            TableName = tableName,
            KeyValues = JsonSerializer.Serialize(keyValues),
            OldValues = oldValues.Count == 0 ? null : JsonSerializer.Serialize(oldValues),
            NewValues = newValues.Count == 0 ? null : JsonSerializer.Serialize(newValues),
            ChangedColumns = changedColumns.Count == 0 ? null : JsonSerializer.Serialize(changedColumns)
        };
    }

    private void SaveAuditChangesInternal(Audit audit)
    {
        SaveAudit?.Invoke(this, new SaveAuditEventArgs(audit));
        SaveAuditChanges(audit);
    }

    public class SaveAuditEventArgs(Audit audit) : EventArgs
    {

        public Audit Audit { get; } = audit;
    }
}

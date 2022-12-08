using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Goal.Seedwork.Infra.Data.Auditing
{
    public abstract class AuditChangesInterceptor : SaveChangesInterceptor
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly ILogger logger;

        protected Audit _audit;

        protected AuditChangesInterceptor(
            IHttpContextAccessor httpContextAccessor,
            ILogger logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        protected virtual string CurrentPrincipal
            => httpContextAccessor?.HttpContext?.User?.Identity?.Name;

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = new CancellationToken())
            => await Task.Run(() => SavingChanges(eventData, result), cancellationToken);

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
            => await Task.Run(() => SavedChanges(eventData, result), cancellationToken);

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            _audit.Succeeded = true;
            _audit.EndTime = DateTime.UtcNow;

            SaveAuditChanges(_audit);

            return result;
        }

        public override async Task SaveChangesFailedAsync(
            DbContextErrorEventData eventData,
            CancellationToken cancellationToken = new CancellationToken())
            => await Task.Run(() => SaveChangesFailed(eventData), cancellationToken);

        public override void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            _audit.Succeeded = false;
            _audit.EndTime = DateTime.UtcNow;
            _audit.ErrorMessage = eventData.Exception.Message;

            SaveAuditChanges(_audit);
        }

        protected virtual async Task SaveAuditChangesAsync(Audit audit, CancellationToken cancellationToken = new CancellationToken())
            => await Task.Run(() => SaveAuditChanges(audit), cancellationToken);

        protected virtual void SaveAuditChanges(Audit audit) => logger.LogInformation($"Saving audit changes: {audit}");

        private Audit CreateAudit(DbContext context)
        {
            context.ChangeTracker.DetectChanges();
            var audit = new Audit { StartTime = DateTimeOffset.UtcNow };

            foreach (EntityEntry entry in context.ChangeTracker
                .Entries()
                .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted))
            {
                audit.Entries.Add(CreateAuditEntry(entry));
            }

            return audit;
        }

        private AuditEntry CreateAuditEntry(EntityEntry entry)
        {
            string tableName = entry.Metadata.GetTableName();
            string schema = entry.Metadata.GetSchema();

            var keyValues = new Dictionary<string, object>();
            var oldValues = new Dictionary<string, object>();
            var newValues = new Dictionary<string, object>();
            var changedColumns = new List<string>();
            AuditType auditType = AuditType.None;

            var identifier = StoreObjectIdentifier.Table(tableName, schema);

            foreach (PropertyEntry property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                string columnName = property.Metadata.GetColumnName(identifier);

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

                    case EntityState.Modified:
                        if (property.IsModified && !property.OriginalValue.Equals(property.CurrentValue))
                        {
                            changedColumns.Add(columnName);

                            oldValues[propertyName] = property.OriginalValue;
                            newValues[propertyName] = property.CurrentValue;
                            auditType = AuditType.Update;
                        }
                        break;
                }
            }

            return new AuditEntry
            {
                AuditType = auditType.ToString(),
                AuditUser = CurrentPrincipal,
                TableName = tableName,
                KeyValues = JsonConvert.SerializeObject(keyValues),
                OldValues = oldValues.Count == 0
                    ? null
                    : JsonConvert.SerializeObject(oldValues),
                NewValues = newValues.Count == 0
                    ? null
                    : JsonConvert.SerializeObject(newValues),
                ChangedColumns = changedColumns.Count == 0
                    ? null
                    : JsonConvert.SerializeObject(changedColumns)
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Goal.Infra.Data.Seedwork.Auditing
{
    public abstract class AuditChangesInterceptor : SaveChangesInterceptor
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly ILogger logger;

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
        {
            try
            {
                DbContext context = eventData.Context;
                context.ChangeTracker.DetectChanges();

                var auditEntries = new List<AuditEntry>();

                foreach (EntityEntry entry in context.ChangeTracker
                    .Entries()
                    .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted))
                {
                    auditEntries.Add(new AuditEntry(entry, CurrentPrincipal));
                }

                IEnumerable<Audit> audits = auditEntries
                    .Select(x => x.ToAudit())
                    .ToList();

                await SaveAuditChangesAsync(audits);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Fail do index audit logs.", ex.Message);
            }

            return result;
        }

        protected abstract Task SaveAuditChangesAsync(IEnumerable<Audit> audits);
    }
}

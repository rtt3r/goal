using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Z.EntityFramework.Plus;

namespace Goal.Infra.Data.Seedwork
{
    public abstract class GoalDbContext : DbContext
    {
        public override int SaveChanges()
        {
            var audit = new Audit
            {
                CreatedBy = ""
            };

            return this.SaveChanges(audit);
        }
    }

    public class ElasticAuditChangesInterceptor : EFAuditChangesInterceptor
    {
        public ElasticAuditChangesInterceptor(
            IHttpContextAccessor httpContextAccessor,
            ILogger<EFAuditChangesInterceptor> logger)
            : base(httpContextAccessor, logger)
        {
        }

        protected override Task SaveAuditEntriesAsync(IEnumerable<Audit> audits) => throw new System.NotImplementedException();
    }
}

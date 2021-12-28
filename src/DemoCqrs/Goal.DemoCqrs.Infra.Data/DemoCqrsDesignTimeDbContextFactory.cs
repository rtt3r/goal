using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Goal.DemoCqrs.Infra.Data
{
    public class DemoCqrsDesignTimeDbContextFactory : DesignTimeDbContextFactory<DemoCqrsContext>
    {
        protected override DemoCqrsContext CreateNewInstance(DbContextOptionsBuilder<DemoCqrsContext> optionsBuilder, string connectionString)
        {
            optionsBuilder.UseSqlite(connectionString);
            return new DemoCqrsContext(optionsBuilder.Options);
        }
    }
}

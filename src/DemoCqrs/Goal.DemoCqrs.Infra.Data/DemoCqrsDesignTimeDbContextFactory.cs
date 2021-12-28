using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Goal.DemoCqrsCqrs.Infra.Data
{
    public class DemoCqrsCqrsDesignTimeDbContextFactory : DesignTimeDbContextFactory<DemoCqrsCqrsContext>
    {
        protected override DemoCqrsCqrsContext CreateNewInstance(DbContextOptionsBuilder<DemoCqrsCqrsContext> optionsBuilder, string connectionString)
        {
            optionsBuilder.UseSqlite(connectionString);
            return new DemoCqrsCqrsContext(optionsBuilder.Options);
        }
    }
}

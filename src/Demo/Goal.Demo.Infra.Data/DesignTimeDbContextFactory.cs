using Microsoft.EntityFrameworkCore;
using Goal.Infra.Data;

namespace Goal.Demo.Infra.Data
{
    public class DesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<SampleContext>
    {
        protected override SampleContext CreateNewInstance(DbContextOptionsBuilder<SampleContext> optionsBuilder, string connectionString)
        {
            optionsBuilder.UseSqlite(connectionString);
            return new SampleContext(optionsBuilder.Options);
        }
    }
}

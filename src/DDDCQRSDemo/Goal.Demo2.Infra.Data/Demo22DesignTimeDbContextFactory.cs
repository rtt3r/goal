using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Goal.Demo22.Infra.Data
{
    public class Demo22DesignTimeDbContextFactory : DesignTimeDbContextFactory<Demo22Context>
    {
        protected override Demo22Context CreateNewInstance(DbContextOptionsBuilder<Demo22Context> optionsBuilder, string connectionString)
        {
            optionsBuilder.UseSqlite(connectionString);
            return new Demo22Context(optionsBuilder.Options);
        }
    }
}

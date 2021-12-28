using Goal.DemoCqrs.Domain.Aggregates.People;
using Goal.DemoCqrs.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Goal.DemoCqrs.Infra.Data
{
    public class DemoCqrsContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DemoCqrsContext(DbContextOptions<DemoCqrsContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        }
    }
}

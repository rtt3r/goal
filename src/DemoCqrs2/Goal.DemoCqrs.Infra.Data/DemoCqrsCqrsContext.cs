using Goal.DemoCqrsCqrs.Domain.Aggregates.People;
using Goal.DemoCqrsCqrs.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Goal.DemoCqrsCqrs.Infra.Data
{
    public class DemoCqrsCqrsContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DemoCqrsCqrsContext(DbContextOptions<DemoCqrsCqrsContext> options)
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

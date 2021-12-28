using Microsoft.EntityFrameworkCore;
using Ritter.Infra.Data;
using Goal.Demo.Domain.Aggregates.People;

namespace Goal.Demo.Infra.Data
{
    public class SampleContext : EFUnitOfWork, IEFUnitOfWork, ISql
    {
        public DbSet<Person> People { get; set; }

        public SampleContext(DbContextOptions<SampleContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        }
    }
}

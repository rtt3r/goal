using Goal.Demo22.Domain.Aggregates.People;
using Goal.Demo22.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Goal.Demo22.Infra.Data
{
    public class Demo22Context : DbContext
    {
        public DbSet<Person> People { get; set; }

        public Demo22Context(DbContextOptions<Demo22Context> options)
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

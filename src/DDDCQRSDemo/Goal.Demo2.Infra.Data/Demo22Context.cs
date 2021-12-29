using Goal.Demo2.Domain.Aggregates.People;
using Goal.Demo2.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Goal.Demo2.Infra.Data
{
    public class Demo2Context : DbContext
    {
        public DbSet<Person> People { get; set; }

        public Demo2Context(DbContextOptions<Demo2Context> options)
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

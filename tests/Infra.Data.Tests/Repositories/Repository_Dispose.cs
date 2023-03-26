using System;
using System.ComponentModel.DataAnnotations.Schema;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.Repositories
{
    public class Repository_Dispose
    {
        [Fact]
        public void DisposeSimpleRepository_GivenTrue()
        {
            // Arrange
            var repository = new TestRepository();

            // Act
            repository.Dispose();

            // Assert
            repository.IsDisposed.Should().BeTrue();
        }

        [Fact]
        public void DisposeSingularityRepository_GivenTrue()
        {
            // Arrange
            UniverseContext ctx = CreateContext();
            var repository = new SingularityRepository(ctx);

            // Act
            repository.Dispose();

            // Assert
            repository.Context.Should().BeNull();
        }

        public UniverseContext CreateContext()
            => new(CreateOptions());

        private DbContextOptions CreateOptions()
        {
            return new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        public class UniverseContext : DbContext
        {
            public UniverseContext(DbContextOptions options)
                : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder
                    .Entity<Singularity>()
                    .HasData(
                        new Singularity { Id = 77, Type = "Black Hole" },
                        new Singularity { Id = 88, Type = "Bing Bang" });
            }
        }

        public class Singularity
        {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }

            public string Type { get; set; }
        }

        private class TestRepository : Repository
        {
            public bool IsDisposed { get; private set; }

            protected override void Dispose(bool disposing)
                => IsDisposed = true;
        }

        private class SingularityRepository : Repository<Singularity>
        {
            public SingularityRepository(DbContext context)
                : base(context)
            {
            }
        }
    }
}

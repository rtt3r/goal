using System;
using System.ComponentModel.DataAnnotations.Schema;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Goal.Infra.Data.Tests.Repositories;

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
        repository.PublicContext.Should().BeNull();
    }

    public static UniverseContext CreateContext()
        => new(CreateOptions());

    private static DbContextOptions CreateOptions()
    {
        return new DbContextOptionsBuilder()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    public class UniverseContext(DbContextOptions options) : DbContext(options)
    {
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

        public string? Type { get; set; }
    }

    private class TestRepository : Repository
    {
        public bool IsDisposed { get; private set; }

        protected override void Dispose(bool disposing)
            => IsDisposed = true;
    }

    private class SingularityRepository(DbContext context) : Repository<Singularity>(context)
    {
        public DbContext PublicContext => Context;
    }
}

using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.DesignTimeDbContextFactory;

public class DesignTimeDbContextFactory_CreateDBContext
{
    [Fact]
    public void ReturnsValidContext_GivenCustomConnectionString()
    {
        // Arrange
        var factory = new InMemoryDbContextFactory();

        // Act
        TestDbContext context = factory.CreateDbContext(Array.Empty<string>());

        // Assert
        context.Should().NotBeNull();
    }

    [Fact]
    public void ReturnsValidContext_GivenDefaultConnectionString()
    {
        // Arrange
        var factory = new DefaultDbContextFactory();

        // Act
        TestDbContext context = factory.CreateDbContext(Array.Empty<string>());

        // Assert
        context.Should().NotBeNull();
    }

    [Fact]
    public void ThrowsException_WhenConnectionStringNotFound()
    {
        // Arrange
        var factory = new NotFoundDbContextFactory();

        // Act & Assert
        FluentActions.Invoking(() => factory.CreateDbContext(Array.Empty<string>()))
            .Should().Throw<ArgumentNullException>().WithMessage($"Value cannot be null. (Parameter 'databaseName')");
    }

    private class InMemoryDbContextFactory : DesignTimeDbContextFactory<TestDbContext>
    {
        protected override string ConnectionStringName => "InMemoryDatabaseName";

        protected override TestDbContext CreateNewInstance(DbContextOptionsBuilder<TestDbContext> optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: Configuration.GetConnectionString(ConnectionStringName)!);
            return new TestDbContext(optionsBuilder.Options);
        }
    }

    private class DefaultDbContextFactory : DesignTimeDbContextFactory<TestDbContext>
    {
        protected override TestDbContext CreateNewInstance(DbContextOptionsBuilder<TestDbContext> optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: Configuration.GetConnectionString(ConnectionStringName)!);
            return new TestDbContext(optionsBuilder.Options);
        }
    }

    private class NotFoundDbContextFactory : DesignTimeDbContextFactory<TestDbContext>
    {
        protected override string ConnectionStringName => "NotFoundDatabaseName";

        protected override TestDbContext CreateNewInstance(DbContextOptionsBuilder<TestDbContext> optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: Configuration.GetConnectionString(ConnectionStringName)!);
            return new TestDbContext(optionsBuilder.Options);
        }
    }
}

public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
{
}

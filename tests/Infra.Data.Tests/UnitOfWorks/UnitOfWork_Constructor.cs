using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Goal.Infra.Data.Tests.UnitOfWorks;

public class UnitOfWork_Constructor
{
    public class MockUnitOfWork(DbContext context) : UnitOfWork(context)
    {
    }

    [Fact]
    public void ContextIsNull_ThrowsArgumentNullException()
    {
        // Act and Assert
        FluentActions.Invoking(() => new MockUnitOfWork(null!))
            .Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'context')");
    }

    [Fact]
    public void ContextIsNotNull_CreatesAnInstanceOfUnitOfWork()
    {
        var dbContext = new DbContext(CreateOptions());
        var unitOfWork = new MockUnitOfWork(dbContext);

        unitOfWork.Should().NotBeNull();
    }

    private static DbContextOptions CreateOptions()
    {
        return new DbContextOptionsBuilder()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }
}
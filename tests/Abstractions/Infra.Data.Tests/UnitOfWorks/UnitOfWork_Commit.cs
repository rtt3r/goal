using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Tests.UnitOfWorks;

public class UnitOfWork_Commit
{
    public class MockDbContext : DbContext
    {
    }

    public class MockUnitOfWork(UnitOfWork_Commit.MockDbContext context) : UnitOfWork(context)
    {
    }

    [Fact]
    public void Called_ReturnsExpectedResult()
    {
        // Arrange
        var contextMock = new Mock<MockDbContext>();
        var unitOfWork = new MockUnitOfWork(contextMock.Object);
        contextMock.Setup(c => c.SaveChanges()).Returns(1);

        // Act
        int result = unitOfWork.Commit();

        // Assert
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void CalledWithZeroChanges_ReturnsFalse()
    {
        // Arrange
        var contextMock = new Mock<MockDbContext>();
        var unitOfWork = new MockUnitOfWork(contextMock.Object);
        contextMock.Setup(c => c.SaveChanges()).Returns(0);

        // Act
        int result = unitOfWork.Commit();

        // Assert
        result.Should().Be(0);
    }
}
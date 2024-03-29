using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Tests.UnitOfWorks;

public class UnitOfWork_Save
{
    public class MockDbContext : DbContext
    {
    }

    public class MockUnitOfWork(UnitOfWork_Save.MockDbContext context) : UnitOfWork(context)
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
        bool result = unitOfWork.Save();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CalledWithZeroChanges_ReturnsFalse()
    {
        // Arrange
        var contextMock = new Mock<MockDbContext>();
        var unitOfWork = new MockUnitOfWork(contextMock.Object);
        contextMock.Setup(c => c.SaveChanges()).Returns(0);

        // Act
        bool result = unitOfWork.Save();

        // Assert
        result.Should().BeFalse();
    }
}
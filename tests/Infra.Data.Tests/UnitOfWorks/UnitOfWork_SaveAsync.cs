using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.UnitOfWorks;

public class UnitOfWork_SaveAsync
{
    public class MockUnitOfWork(DbContext context) : UnitOfWork(context)
    {
    }

    [Fact]
    public async Task Called_ReturnsExpectedResult()
    {
        // Arrange
        var contextMock = new Mock<DbContext>();
        var unitOfWork = new MockUnitOfWork(contextMock.Object);
        contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        bool result = await unitOfWork.SaveAsync();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task CalledWithCancelledToken_ReturnsZero()
    {
        // Arrange
        var contextMock = new Mock<DbContext>();
        var unitOfWork = new MockUnitOfWork(contextMock.Object);
        var cancellationToken = new CancellationToken(true);

        // Act
        bool result = await unitOfWork.SaveAsync(cancellationToken);

        // Assert
        result.Should().BeFalse();
    }
}
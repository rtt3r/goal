using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Tests.UnitOfWorks;

public class UnitOfWork_CommitAsync
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
        int result = await unitOfWork.CommitAsync();

        // Assert
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task CalledWithCancelledToken_ReturnsZero()
    {
        // Arrange
        var contextMock = new Mock<DbContext>();
        var unitOfWork = new MockUnitOfWork(contextMock.Object);
        var cancellationToken = new CancellationToken(true);

        // Act
        int result = await unitOfWork.CommitAsync(cancellationToken);

        // Assert
        result.Should().Be(0);
    }
}
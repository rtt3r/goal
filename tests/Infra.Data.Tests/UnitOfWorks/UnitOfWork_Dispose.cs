using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.UnitOfWorks;

public class UnitOfWork_Dispose
{
    public class MockUnitOfWork : UnitOfWork
    {
        public MockUnitOfWork(DbContext context) : base(context)
        {
        }
    }

    [Fact]
    public void CalledTwice_DoesNotThrowException()
    {
        // Arrange
        var contextMock = new Mock<DbContext>();
        var unitOfWork = new MockUnitOfWork(contextMock.Object);

        // Act
        unitOfWork.Dispose();
        Action act = unitOfWork.Dispose;

        // Assert
        act.Should().NotThrow<ObjectDisposedException>();
    }

    [Fact]
    public void CalledOnce_DisposesTheContext()
    {
        // Arrange
        var contextMock = new Mock<DbContext>();
        var unitOfWork = new MockUnitOfWork(contextMock.Object);

        // Act
        unitOfWork.Dispose();

        // Assert
        contextMock.Verify(c => c.Dispose(), Times.Once);
    }
}
using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.UnitOfWorks
{
    public class UnitOfWork_Constructor
    {
        public class MockUnitOfWork : UnitOfWork
        {
            public MockUnitOfWork(DbContext context) : base(context)
            {
            }
        }

        [Fact]
        public void ContextIsNull_ThrowsArgumentNullException()
        {
            // Act and Assert
            FluentActions.Invoking(() => new MockUnitOfWork(null))
                .Should().Throw<ArgumentNullException>()
                .WithMessage("Object value cannot be null (Parameter 'context')");
        }

        [Fact]
        public void ContextIsNotNull_CreatesAnInstanceOfUnitOfWork()
        {
            var dbContext = new DbContext(GetDbContextOptions());
            var unitOfWork = new MockUnitOfWork(dbContext);

            unitOfWork.Should().NotBeNull();
        }

        private static DbContextOptions GetDbContextOptions()
        {
            return new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}
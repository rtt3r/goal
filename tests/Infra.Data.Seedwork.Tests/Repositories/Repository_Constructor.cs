using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Vantage.Infra.Data.Seedwork.Tests.Mocks;
using Xunit;

namespace Vantage.Infra.Data.Seedwork.Tests.Repositories
{
    public class Repository_Constructor
    {
        [Fact]
        public void NotThrowsAnyExceptionGivenSimpleRepository()
        {
            var mockDbContext = new Mock<DbContext>();
            var testRepository = new TestRepository(mockDbContext.Object);

            testRepository.Context.Should().NotBeNull().And.BeOfType(mockDbContext.Object.GetType());
        }

        [Fact]
        public void NotThrowsAnyExceptionGivenGenericRepository()
        {
            var mockDbContext = new Mock<DbContext>();
            var testRepository = new TestRepository(mockDbContext.Object);

            testRepository.Context.Should().NotBeNull().And.BeOfType(mockDbContext.Object.GetType());
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenSimpleRepository()
        {
            Action act = () => { _ = new TestRepository(null); };
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("context");
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenGenericRepository()
        {
            Action act = () => { _ = new TestRepository(null); };
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("context");
        }
    }
}
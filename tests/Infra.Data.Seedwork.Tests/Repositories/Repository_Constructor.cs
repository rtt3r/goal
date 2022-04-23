using System;
using FluentAssertions;
using Goal.Infra.Data.Seedwork.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Seedwork.Tests.Repositories
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
        public void ThrowsArgumentNullExceptionGivenSimpleRepository()
        {
            Action act = () => { _ = new TestRepository(null); };
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("context");
        }
    }
}
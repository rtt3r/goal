using System;
using FluentAssertions;
using Goal.Seedwork.Infra.Data.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.Repositories;

public class Repository_Constructor
{
    [Fact]
    public void NotThrowsAnyExceptionGivenSimpleRepository()
    {
        var mockDbContext = new Mock<DbContext>();
        var testRepository = new TestRepository(mockDbContext.Object);

        testRepository.PublicContext.Should().NotBeNull().And.BeOfType(mockDbContext.Object.GetType());
    }

    [Fact]
    public void ThrowsArgumentNullExceptionGivenSimpleRepository()
    {
        Action act = () => _ = new TestRepository(null!);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("context");
    }
}
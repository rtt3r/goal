using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Goal.Seedwork.Infra.Data.Tests.Extensions;
using Goal.Seedwork.Infra.Data.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.Repositories;

public class Repository_Add
{
    [Fact]
    public void CallSaveChangesSuccessfullyGivenOneEntity()
    {
        var mockedTests = new List<Test>();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        var test = new Test(1);
        testRepository.Add(test);

        mockDbContext.Verify(x => x.Set<Test>().Add(It.IsAny<Test>()), Times.Once);
    }

    [Fact]
    public void CallSaveChangesSuccessfullyGivenOneEntityAsync()
    {
        var mockedTests = new List<Test>();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        var test = new Test(1);
        testRepository.AddAsync(test).GetAwaiter().GetResult();

        mockDbContext.Verify(x => x.Set<Test>().AddAsync(It.IsAny<Test>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void ThrowsArgumentNullExceptionGivenNullEntity()
    {
        var mockDbContext = new Mock<DbContext>();

        Action act = () =>
        {
            var testRepository = new TestRepository(mockDbContext.Object);
            testRepository.Add((Test)null!);
        };

        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("entity");
    }

    [Fact]
    public void ThrowsArgumentNullExceptionGivenNullEntityAsync()
    {
        var mockDbContext = new Mock<DbContext>();

        Action act = () =>
        {
            var testRepository = new TestRepository(mockDbContext.Object);
            testRepository.AddAsync((Test)null!).GetAwaiter().GetResult();
        };

        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("entity");
    }

    [Fact]
    public void CallSaveChangesSuccessfullyGivenManyEntities()
    {
        var mockedTests = new List<Test>();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        List<Test> tests = MockTests();
        testRepository.Add(tests);

        mockDbContext.Verify(x => x.Set<Test>().AddRange(It.IsAny<IEnumerable<Test>>()), Times.Once);
    }

    [Fact]
    public void ThrowsArgumentNullExceptionGivenNullEntityEnumerable()
    {
        var mockDbContext = new Mock<DbContext>();

        Action act = () =>
        {
            var testRepository = new TestRepository(mockDbContext.Object);
            testRepository.Add((IEnumerable<Test>)null!);
        };

        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("entities");
    }

    [Fact]
    public void ThrowsArgumentNullExceptionGivenNullEntityEnumerableAsync()
    {
        var mockDbContext = new Mock<DbContext>();

        Action act = () =>
        {
            var testRepository = new TestRepository(mockDbContext.Object);
            testRepository.AddAsync((IEnumerable<Test>)null!).GetAwaiter().GetResult();
        };

        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("entities");
    }

    [Fact]
    public void CallSaveChangesSuccessfullyGivenManyEntitiesAsync()
    {
        var mockedTests = new List<Test>();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        List<Test> tests = MockTests();
        testRepository.AddAsync(tests).GetAwaiter().GetResult();

        mockDbContext.Verify(x => x.Set<Test>().AddRangeAsync(It.IsAny<IEnumerable<Test>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    private static List<Test> MockTests(int count)
    {
        var tests = new List<Test>();

        for (int i = 1; i <= count; i++)
        {
            tests.Add(new Test(i));
        }

        return tests;
    }

    private static List<Test> MockTests() => MockTests(5);
}

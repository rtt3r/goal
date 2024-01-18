using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Goal.Infra.Data.Tests.Extensions;
using Goal.Infra.Data.Tests.Mocks;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Tests.Repositories;

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
    public async Task CallSaveChangesSuccessfullyGivenOneEntityAsync()
    {
        var mockedTests = new List<Test>();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        var test = new Test(1);
        await testRepository.AddAsync(test);

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

        act.Should().Throw<ArgumentNullException>().WithParameterName("entity");
    }

    [Fact]
    public async Task ThrowsArgumentNullExceptionGivenNullEntityAsync()
    {
        var mockDbContext = new Mock<DbContext>();

        await FluentActions.Invoking(() =>
        {
            var testRepository = new TestRepository(mockDbContext.Object);
            return testRepository.AddAsync((Test)null!);
        })
        .Should().ThrowAsync<ArgumentNullException>().WithParameterName("entity");
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

        act.Should().Throw<ArgumentNullException>().WithParameterName("entities");
    }

    [Fact]
    public async Task ThrowsArgumentNullExceptionGivenNullEntityEnumerableAsync()
    {
        var mockDbContext = new Mock<DbContext>();

        await FluentActions.Invoking(() =>
        {
            var testRepository = new TestRepository(mockDbContext.Object);
            return testRepository.AddAsync((IEnumerable<Test>)null!);
        })
        .Should().ThrowAsync<ArgumentNullException>().WithParameterName("entities");
    }

    [Fact]
    public async Task CallSaveChangesSuccessfullyGivenManyEntitiesAsync()
    {
        var mockedTests = new List<Test>();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        List<Test> tests = MockTests();
        await testRepository.AddAsync(tests);

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

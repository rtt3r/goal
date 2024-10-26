using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Infra.Data.Tests.Extensions;
using Goal.Infra.Data.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Tests.Repositories;

public class Repository_List
{
    [Fact]
    public void ReturnsAllEntities()
    {
        List<Test> mockedTests = MockTests();

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        ICollection<Test> tests = testRepository.List();

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveSameCount(mockedTests);
    }

    [Fact]
    public void ReturnsEmpty()
    {
        Mock<DbSet<Test>> mockDbSet = Enumerable.Empty<Test>()
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        ICollection<Test> tests = testRepository.List();

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task ReturnsAllEntitiesAsync()
    {
        List<Test> mockedTests = MockTests();

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        ICollection<Test> tests = await testRepository.ListAsync();

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveSameCount(mockedTests);
    }

    [Fact]
    public async Task ReturnsEmptyAsync()
    {
        Mock<DbSet<Test>> mockDbSet = Enumerable.Empty<Test>()
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        ICollection<Test> tests = await testRepository.ListAsync();

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.BeEmpty();
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

    private static List<Test> MockTests()
        => MockTests(5);
}

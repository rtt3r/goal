using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Seedwork.Infra.Data.Tests.Extensions;
using Goal.Seedwork.Infra.Data.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.Repositories;

public class Repository_Get
{
    [Fact]
    public void ReturnsAnEntityGivenId()
    {
        List<Test> tests = MockTests();

        Mock<DbSet<Test>> mockDbSet = tests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);

        string id = tests[2].Id;

        Test? test = testRepository.Load(id);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        test.Should().NotBeNull();
        test?.Id.Should().Be(id);
    }

    [Fact]
    public void ReturnsNullGivenId()
    {
        List<Test> tests = MockTests();

        Mock<DbSet<Test>> mockDbSet = tests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();

        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        Test? test = testRepository.Load(Guid.NewGuid().ToString());

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        test.Should().BeNull();
    }

    [Fact]
    public async Task ReturnsAnEntityGivenIdAsync()
    {
        List<Test> tests = MockTests();

        Mock<DbSet<Test>> mockDbSet = tests
            .AsQueryable()
            .BuildMockDbSet();

        mockDbSet.Setup(s => s.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>())).ReturnsAsync(tests[2]);

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        string id = tests[2].Id;

        var testRepository = new TestRepository(mockDbContext.Object);
        Test? test = await testRepository.LoadAsync(id);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        test.Should().NotBeNull();
        test?.Id.Should().Be(id);
    }

    [Fact]
    public async Task ReturnsNullGivenIdAsync()
    {
        List<Test> tests = MockTests();

        Mock<DbSet<Test>> mockDbSet = tests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        Test? test = await testRepository.LoadAsync(Guid.NewGuid().ToString());

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        test.Should().BeNull();
    }

    private static List<Test> MockTests()
    {
        return
        [
            new Test(1),
            new Test(2),
            new Test(3),
            new Test(4),
            new Test(5)
        ];
    }
}

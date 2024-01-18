using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Specifications;
using Goal.Infra.Data.Tests.Extensions;
using Goal.Infra.Data.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Tests.Repositories;

public class Repository_Any
{
    [Fact]
    public void ReturnsTrueGivenAnyEntity()
    {
        List<Test> mockedTests = MockTests();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        bool any = testRepository.Any();

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        any.Should().BeTrue();
    }

    [Fact]
    public async Task ReturnsTrueGivenAnyEntityAsync()
    {
        List<Test> mockedTests = MockTests();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        bool any = await testRepository.AnyAsync();

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        any.Should().BeTrue();
    }

    [Fact]
    public void ReturnsFalseGivenNoneEntity()
    {
        List<Test> mockedTests = MockTests(0);

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        bool any = testRepository.Any();

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        any.Should().BeFalse();
    }

    [Fact]
    public async Task ReturnsFalseGivenNoneEntityAsync()
    {
        List<Test> mockedTests = MockTests(0);

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        var testRepository = new TestRepository(mockDbContext.Object);
        bool any = await testRepository.AnyAsync();

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        any.Should().BeFalse();
    }

    [Fact]
    public void ReturnsTrueGivenAnyActiveEntity()
    {
        List<Test> mockedTests = MockTests();
        mockedTests.First().Deactivate();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        var testRepository = new TestRepository(mockDbContext.Object);
        bool any = testRepository.Any(spec);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        any.Should().BeTrue();
    }

    [Fact]
    public async Task ReturnsTrueGivenAnyActiveEntityAsync()
    {
        List<Test> mockedTests = MockTests();
        mockedTests.First().Deactivate();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        var testRepository = new TestRepository(mockDbContext.Object);
        bool any = await testRepository.AnyAsync(spec);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        any.Should().BeTrue();
    }

    [Fact]
    public void ReturnsFalseGivenNoneActiveEntity()
    {
        List<Test> mockedTests = MockTests(1);
        mockedTests.First().Deactivate();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        var testRepository = new TestRepository(mockDbContext.Object);
        bool any = testRepository.Any(spec);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        any.Should().BeFalse();
    }

    [Fact]
    public async Task ReturnsFalseGivenNoneActiveEntityAsync()
    {
        List<Test> mockedTests = MockTests(1);
        mockedTests.First().Deactivate();

        Mock<DbSet<Test>> mockDbSet = mockedTests.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        var testRepository = new TestRepository(mockDbContext.Object);
        bool any = await testRepository.AnyAsync(spec);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        any.Should().BeFalse();
    }

    [Fact]
    public void ThrowsArgumentNullExceptionGivenNullSpecification()
    {
        var mockDbContext = new Mock<DbContext>();

        Action act = () =>
        {
            ISpecification<Test> spec = null!;
            var testRepository = new TestRepository(mockDbContext.Object);
            testRepository.Any(spec);
        };

        act.Should().Throw<ArgumentNullException>().WithParameterName("specification");
    }

    [Fact]
    public async Task ThrowsArgumentNullExceptionGivenNullSpecificationAsync()
    {
        var mockDbContext = new Mock<DbContext>();

        Func<Task> act = () =>
        {
            ISpecification<Test> spec = null!;
            var testRepository = new TestRepository(mockDbContext.Object);

            return testRepository.AnyAsync(spec);
        };

        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("specification");
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

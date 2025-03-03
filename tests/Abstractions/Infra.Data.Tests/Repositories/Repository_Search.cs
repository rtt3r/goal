using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Crosscutting.Specifications;
using Goal.Infra.Data.Tests.Extensions;
using Goal.Infra.Data.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Tests.Repositories;

public class Repository_Search
{
    [Fact]
    public void ReturnsAllEntitiesGivenSpecification()
    {
        List<Test> mockedTests = MockTests();
        mockedTests.Skip(1).Take(2).ForEach(t => t.Deactivate());

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        var testRepository = new TestRepository(mockDbContext.Object);
        ICollection<Test> tests = testRepository.Search(spec);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveSameCount(mockedTests.Where(p => p.Active));
    }

    [Fact]
    public void ThrowsArgumentNullExceptionGivenNullSpecification()
    {
        List<Test> mockedTests = MockTests();
        mockedTests.Skip(1).Take(2).ForEach(t => t.Deactivate());

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        Action act = () =>
        {
            ISpecification<Test> spec = null!;
            var testRepository = new TestRepository(mockDbContext.Object);
            testRepository.Search(spec);
        };

        act.Should().Throw<ArgumentNullException>().WithParameterName("specification");
    }

    [Fact]
    public async Task ReturnsAllEntitiesGivenSpecificationAsync()
    {
        List<Test> mockedTests = MockTests();
        mockedTests.Skip(1).Take(2).ForEach(t => t.Deactivate());

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        var testRepository = new TestRepository(mockDbContext.Object);
        ICollection<Test> tests = await testRepository.SearchAsync(spec);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveSameCount(mockedTests.Where(p => p.Active));
    }

    [Fact]
    public async Task ThrowsArgumentNullExceptionGivenNullSpecificationAsync()
    {
        var mockDbContext = new Mock<DbContext>();

        Func<Task> act = () =>
        {
            ISpecification<Test> spec = null!;
            var testRepository = new TestRepository(mockDbContext.Object);
            return testRepository.SearchAsync(spec);
        };

        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("specification");
    }

    [Fact]
    public void ReturnsEmptyGivenSpecification()
    {
        List<Test> mockedTests = MockTests();

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Id == Guid.NewGuid().ToString());
        var testRepository = new TestRepository(mockDbContext.Object);
        ICollection<Test> tests = testRepository.Search(spec);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task ReturnsEmptyGivenSpecificationAsync()
    {
        List<Test> mockedTests = MockTests();

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Id == Guid.NewGuid().ToString());

        var testRepository = new TestRepository(mockDbContext.Object);
        ICollection<Test> tests = await testRepository.SearchAsync(spec);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public void ReturnsFirstPageGivenPageSize10AndTotal100()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Search(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[0]);
        tests.Last().Should().Be(mockedTests[9]);
    }

    [Fact]
    public async Task ReturnsFirstPageGivenPageSize10AndTotal100Async()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.SearchAsync(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[0]);
        tests.Last().Should().Be(mockedTests[9]);
    }

    [Fact]
    public void ReturnsSecondPageGivenPageSize10AndTotal100()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(1, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Search(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[10]);
        tests.Last().Should().Be(mockedTests[19]);
    }

    [Fact]
    public async Task ReturnsSecondPageGivenPageSize10AndTotal100Async()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(1, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.SearchAsync(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[10]);
        tests.Last().Should().Be(mockedTests[19]);
    }

    [Fact]
    public void ReturnsFirstPageGivenPageSize10AndTotal9()
    {
        List<Test> mockedTests = MockTests(9);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Search(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(9);
        tests.TotalCount.Should().Be(9);
        tests.First().Should().Be(mockedTests[0]);
        tests.Last().Should().Be(mockedTests[8]);
    }

    [Fact]
    public async Task ReturnsFirstPageGivenPageSize10AndTotal9Async()
    {
        List<Test> mockedTests = MockTests(9);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.SearchAsync(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(9);
        tests.TotalCount.Should().Be(9);
        tests.First().Should().Be(mockedTests[0]);
        tests.Last().Should().Be(mockedTests[8]);
    }

    [Fact]
    public void ReturnsFirstPageActivesGivenPageSize10AndTotal100()
    {
        List<Test> mockedTests = MockTests(100);
        mockedTests.First().Deactivate();
        mockedTests.Last().Deactivate();

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        IPageSearch pageSearch = new PageSearch(0, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Search(spec, pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(10);
        tests.TotalCount.Should().Be(98);
        tests.First().Should().Be(mockedTests[1]);
        tests.Last().Should().Be(mockedTests[10]);
    }

    [Fact]
    public void GivenDefaultPageThenReturnsFirstPageActivesAndTotal100()
    {
        List<Test> mockedTests = MockTests(100);
        mockedTests.First().Deactivate();
        mockedTests.Last().Deactivate();

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        IPageSearch pageSearch = new PageSearch(-1, -1);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Search(spec, pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(98);
        tests.TotalCount.Should().Be(98);
        tests.First().Should().Be(mockedTests[1]);
        tests.Last().Should().Be(mockedTests[98]);
    }

    [Fact]
    public void ThrowsArgumentNullExceptionGivenSpecificationAndNullPagination()
    {
        var mockDbContext = new Mock<DbContext>();

        Action act = () =>
        {
            ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
            IPageSearch pageSearch = null!;
            var testRepository = new TestRepository(mockDbContext.Object);
            testRepository.Search(spec, pageSearch);
        };

        act.Should().Throw<ArgumentNullException>().WithParameterName("pageSearch");
    }

    [Fact]
    public async Task ThrowsArgumentNullExceptionGivenSpecificationAndNullPaginationAsync()
    {
        var mockDbContext = new Mock<DbContext>();

        Func<Task> act = () =>
        {
            ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
            IPageSearch pageSearch = null!;
            var testRepository = new TestRepository(mockDbContext.Object);

            return testRepository.SearchAsync(spec, pageSearch);
        };

        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("pageSearch");
    }

    [Fact]
    public void ThrowsArgumentNullExceptionGivenPaginationAndNullSpecification()
    {
        var mockDbContext = new Mock<DbContext>();

        Action act = () =>
        {
            ISpecification<Test> spec = null!;
            IPageSearch pageSearch = new PageSearch(0, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            testRepository.Search(spec, pageSearch);
        };

        act.Should().Throw<ArgumentNullException>().WithParameterName("specification");
    }

    [Fact]
    public async Task ThrowsArgumentNullExceptionGivenPaginationAndNullSpecificationAsync()
    {
        var mockDbContext = new Mock<DbContext>();

        Func<Task> act = () =>
        {
            ISpecification<Test> spec = null!;
            IPageSearch pageSearch = new PageSearch(0, 10);
            var testRepository = new TestRepository(mockDbContext.Object);

            return testRepository.SearchAsync(spec, pageSearch);
        };

        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("specification");
    }

    [Fact]
    public async Task ReturnsFirstPageActivesGivenPageSize10AndTotal100Async()
    {
        List<Test> mockedTests = MockTests(100);
        mockedTests.First().Deactivate();
        mockedTests.Last().Deactivate();

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        IPageSearch pageSearch = new PageSearch(0, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.SearchAsync(spec, pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(10);
        tests.TotalCount.Should().Be(98);
        tests.First().Should().Be(mockedTests[1]);
        tests.Last().Should().Be(mockedTests[10]);
    }

    [Fact]
    public void ReturnsLastPageActivesGivenPageSize10AndTotal100()
    {
        List<Test> mockedTests = MockTests(100);
        mockedTests.First().Deactivate();
        mockedTests.Last().Deactivate();

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        IPageSearch pageSearch = new PageSearch(9, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Search(spec, pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(8);
        tests.TotalCount.Should().Be(98);
        tests.First().Should().Be(mockedTests[91]);
        tests.Last().Should().Be(mockedTests[98]);
    }

    [Fact]
    public async Task ReturnsLastPageActivesGivenPageSize10AndTotal100Async()
    {
        List<Test> mockedTests = MockTests(100);
        mockedTests.First().Deactivate();
        mockedTests.Last().Deactivate();

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
        IPageSearch pageSearch = new PageSearch(9, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.SearchAsync(spec, pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(8);
        tests.TotalCount.Should().Be(98);
        tests.First().Should().Be(mockedTests[91]);
        tests.Last().Should().Be(mockedTests[98]);
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

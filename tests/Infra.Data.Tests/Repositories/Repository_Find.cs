using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Crosscutting.Extensions;
using Goal.Seedwork.Infra.Crosscutting.Specifications;
using Goal.Seedwork.Infra.Data.Tests.Extensions;
using Goal.Seedwork.Infra.Data.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.Repositories;

public class Repository_Find
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
        ICollection<Test> tests = testRepository.Query();

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
        ICollection<Test> tests = testRepository.Query();

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
        ICollection<Test> tests = await testRepository.QueryAsync();

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
        ICollection<Test> tests = await testRepository.QueryAsync();

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.BeEmpty();
    }

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
        ICollection<Test> tests = testRepository.Query(spec);

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
            testRepository.Query(spec);
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
        ICollection<Test> tests = await testRepository.QueryAsync(spec);

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
            return testRepository.QueryAsync(spec);
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

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Id == Guid.NewGuid());
        var testRepository = new TestRepository(mockDbContext.Object);
        ICollection<Test> tests = testRepository.Query(spec);

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

        ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Id == Guid.NewGuid());

        var testRepository = new TestRepository(mockDbContext.Object);
        ICollection<Test> tests = await testRepository.QueryAsync(spec);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public void ReturnsFirstPageOrderedAscendingGivenPageSize10AndTotal100()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Query(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[0]);
        tests.Last().Should().Be(mockedTests[9]);
    }

    [Fact]
    public void ReturnsFirstPageOrderedDescendingGivenPageSize10AndTotal100()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10, "TId", SortDirection.Desc);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Query(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[99]);
        tests.Last().Should().Be(mockedTests[90]);
    }

    [Fact]
    public async Task ReturnsFirstPageOrderedAscendingGivenPageSize10AndTotal100Async()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.QueryAsync(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[0]);
        tests.Last().Should().Be(mockedTests[9]);
    }

    [Fact]
    public async Task ReturnsFirstPageOrderedDescendingGivenPageSize10AndTotal100Async()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10, "TId", SortDirection.Desc);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.QueryAsync(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[99]);
        tests.Last().Should().Be(mockedTests[90]);
    }

    [Fact]
    public void ReturnsSecondPageOrderedAscendingGivenPageSize10AndTotal100()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(1, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Query(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[10]);
        tests.Last().Should().Be(mockedTests[19]);
    }

    [Fact]
    public void ReturnsSecondPageOrderedDescendingGivenPageSize10AndTotal100()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(1, 10, "TId", SortDirection.Desc);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Query(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[89]);
        tests.Last().Should().Be(mockedTests[80]);
    }

    [Fact]
    public async Task ReturnsSecondPageOrderedAscendingGivenPageSize10AndTotal100Async()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(1, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.QueryAsync(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[10]);
        tests.Last().Should().Be(mockedTests[19]);
    }

    [Fact]
    public async Task ReturnsSecondPageOrderedDescendingGivenPageSize10AndTotal100Async()
    {
        List<Test> mockedTests = MockTests(100);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(1, 10, "TId", SortDirection.Desc);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.QueryAsync(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(10);
        tests.TotalCount.Should().Be(100);
        tests.First().Should().Be(mockedTests[89]);
        tests.Last().Should().Be(mockedTests[80]);
    }

    [Fact]
    public void ReturnsFirstPageOrderedAscendingGivenPageSize10AndTotal9()
    {
        List<Test> mockedTests = MockTests(9);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Query(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(9);
        tests.TotalCount.Should().Be(9);
        tests.First().Should().Be(mockedTests[0]);
        tests.Last().Should().Be(mockedTests[8]);
    }

    [Fact]
    public async Task ReturnsFirstPageOrderedAscendingGivenPageSize10AndTotal9Async()
    {
        List<Test> mockedTests = MockTests(9);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.QueryAsync(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(9);
        tests.TotalCount.Should().Be(9);
        tests.First().Should().Be(mockedTests[0]);
        tests.Last().Should().Be(mockedTests[8]);
    }

    [Fact]
    public void ReturnsFirstPageOrderedDescendingGivenPageSize10AndTotal9()
    {
        List<Test> mockedTests = MockTests(9);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10, "TId", SortDirection.Desc);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = testRepository.Query(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(9);
        tests.TotalCount.Should().Be(9);
        tests.First().Should().Be(mockedTests[8]);
        tests.Last().Should().Be(mockedTests[0]);
    }

    [Fact]
    public async Task ReturnsFirstPageOrderedDescendingGivenPageSize10AndTotal9Async()
    {
        List<Test> mockedTests = MockTests(9);

        Mock<DbSet<Test>> mockDbSet = mockedTests
            .AsQueryable()
            .BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

        IPageSearch pageSearch = new PageSearch(0, 10, "TId", SortDirection.Desc);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.QueryAsync(pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNull().And.HaveCount(9);
        tests.TotalCount.Should().Be(9);
        tests.First().Should().Be(mockedTests[8]);
        tests.Last().Should().Be(mockedTests[0]);
    }

    [Fact]
    public void ReturnsFirstPageOrderedAscendingActivesGivenPageSize10AndTotal100()
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
        IPagedList<Test> tests = testRepository.Query(spec, pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(10);
        tests.TotalCount.Should().Be(98);
        tests.First().Should().Be(mockedTests[1]);
        tests.Last().Should().Be(mockedTests[10]);
    }

    [Fact]
    public void GivenDefaultPageThenReturnsFirstPageOrderedAscendingActivesAndTotal100()
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
        IPagedList<Test> tests = testRepository.Query(spec, pageSearch);

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
            testRepository.Query(spec, pageSearch);
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

            return testRepository.QueryAsync(spec, pageSearch);
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
            testRepository.Query(spec, pageSearch);
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

            return testRepository.QueryAsync(spec, pageSearch);
        };

        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("specification");
    }

    [Fact]
    public async Task ReturnsFirstPageOrderedAscendingActivesGivenPageSize10AndTotal100Async()
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
        IPagedList<Test> tests = await testRepository.QueryAsync(spec, pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(10);
        tests.TotalCount.Should().Be(98);
        tests.First().Should().Be(mockedTests[1]);
        tests.Last().Should().Be(mockedTests[10]);
    }

    [Fact]
    public void ReturnsFirstPageOrderedDescendingActivesGivenPageSize10AndTotal100()
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
        IPageSearch pageSearch = new PageSearch(0, 10, "TId", SortDirection.Desc);
        var testRepository = new TestRepository(mockDbContext.Object);

        IPagedList<Test> tests = testRepository.Query(spec, pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(10);
        tests.TotalCount.Should().Be(98);
        tests.First().Should().Be(mockedTests[98]);
        tests.Last().Should().Be(mockedTests[89]);
    }

    [Fact]
    public async Task ReturnsFirstPageOrderedDescendingActivesGivenPageSize10AndTotal100Async()
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
        IPageSearch pageSearch = new PageSearch(0, 10, "TId", SortDirection.Desc);
        var testRepository = new TestRepository(mockDbContext.Object);
        IPagedList<Test> tests = await testRepository.QueryAsync(spec, pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(10);
        tests.TotalCount.Should().Be(98);
        tests.First().Should().Be(mockedTests[98]);
        tests.Last().Should().Be(mockedTests[89]);
    }

    [Fact]
    public void ReturnsLastPageOrderedAscendingActivesGivenPageSize10AndTotal100()
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
        IPagedList<Test> tests = testRepository.Query(spec, pageSearch);

        mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
        tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(8);
        tests.TotalCount.Should().Be(98);
        tests.First().Should().Be(mockedTests[91]);
        tests.Last().Should().Be(mockedTests[98]);
    }

    [Fact]
    public async Task ReturnsLastPageOrderedAscendingActivesGivenPageSize10AndTotal100Async()
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
        IPagedList<Test> tests = await testRepository.QueryAsync(spec, pageSearch);

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

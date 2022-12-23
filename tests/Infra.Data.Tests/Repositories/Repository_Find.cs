using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Crosscutting.Extensions;
using Goal.Seedwork.Infra.Crosscutting.Specifications;
using Goal.Seedwork.Infra.Data.Tests.Extensions;
using Goal.Seedwork.Infra.Data.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.Repositories
{
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
        public void ReturnsAllEntitiesAsync()
        {
            List<Test> mockedTests = MockTests();

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            var testRepository = new TestRepository(mockDbContext.Object);
            ICollection<Test> tests = testRepository.QueryAsync().GetAwaiter().GetResult();

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            tests.Should().NotBeNull().And.HaveSameCount(mockedTests);
        }

        [Fact]
        public void ReturnsEmptyAsync()
        {
            Mock<DbSet<Test>> mockDbSet = Enumerable.Empty<Test>()
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            var testRepository = new TestRepository(mockDbContext.Object);
            ICollection<Test> tests = testRepository.QueryAsync().GetAwaiter().GetResult();

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
                ISpecification<Test> spec = null;
                var testRepository = new TestRepository(mockDbContext.Object);
                testRepository.Query(spec);
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("specification");
        }

        [Fact]
        public void ReturnsAllEntitiesGivenSpecificationAsync()
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
            ICollection<Test> tests = testRepository.QueryAsync(spec).GetAwaiter().GetResult();

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveSameCount(mockedTests.Where(p => p.Active));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenNullSpecificationAsync()
        {
            var mockDbContext = new Mock<DbContext>();

            Action act = () =>
            {
                ISpecification<Test> spec = null;
                var testRepository = new TestRepository(mockDbContext.Object);
                testRepository.QueryAsync(spec).GetAwaiter().GetResult();
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("specification");
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
        public void ReturnsEmptyGivenSpecificationAsync()
        {
            List<Test> mockedTests = MockTests();

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Id == Guid.NewGuid());

            var testRepository = new TestRepository(mockDbContext.Object);
            ICollection<Test> tests = testRepository.QueryAsync(spec).GetAwaiter().GetResult();

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

            ISearchQuery pagination = new SearchQuery(0, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.Query(pagination);

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

            ISearchQuery pagination = new SearchQuery(0, 10, "TId", SortDirection.Desc);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.Query(pagination);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            tests.Should().NotBeNull().And.HaveCount(10);
            tests.TotalCount.Should().Be(100);
            tests.First().Should().Be(mockedTests[99]);
            tests.Last().Should().Be(mockedTests[90]);
        }

        [Fact]
        public void ReturnsFirstPageOrderedAscendingGivenPageSize10AndTotal100Async()
        {
            List<Test> mockedTests = MockTests(100);

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            ISearchQuery pagination = new SearchQuery(0, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.QueryAsync(pagination).GetAwaiter().GetResult();

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            tests.Should().NotBeNull().And.HaveCount(10);
            tests.TotalCount.Should().Be(100);
            tests.First().Should().Be(mockedTests[0]);
            tests.Last().Should().Be(mockedTests[9]);
        }

        [Fact]
        public void ReturnsFirstPageOrderedDescendingGivenPageSize10AndTotal100Async()
        {
            List<Test> mockedTests = MockTests(100);

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            ISearchQuery pagination = new SearchQuery(0, 10, "TId", SortDirection.Desc);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.QueryAsync(pagination).GetAwaiter().GetResult();

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

            ISearchQuery pagination = new SearchQuery(1, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.Query(pagination);

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

            ISearchQuery pagination = new SearchQuery(1, 10, "TId", SortDirection.Desc);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.Query(pagination);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            tests.Should().NotBeNull().And.HaveCount(10);
            tests.TotalCount.Should().Be(100);
            tests.First().Should().Be(mockedTests[89]);
            tests.Last().Should().Be(mockedTests[80]);
        }

        [Fact]
        public void ReturnsSecondPageOrderedAscendingGivenPageSize10AndTotal100Async()
        {
            List<Test> mockedTests = MockTests(100);

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            ISearchQuery pagination = new SearchQuery(1, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.QueryAsync(pagination).GetAwaiter().GetResult();

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            tests.Should().NotBeNull().And.HaveCount(10);
            tests.TotalCount.Should().Be(100);
            tests.First().Should().Be(mockedTests[10]);
            tests.Last().Should().Be(mockedTests[19]);
        }

        [Fact]
        public void ReturnsSecondPageOrderedDescendingGivenPageSize10AndTotal100Async()
        {
            List<Test> mockedTests = MockTests(100);

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            ISearchQuery pagination = new SearchQuery(1, 10, "TId", SortDirection.Desc);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.QueryAsync(pagination).GetAwaiter().GetResult();

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

            ISearchQuery pagination = new SearchQuery(0, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.Query(pagination);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            tests.Should().NotBeNull().And.HaveCount(9);
            tests.TotalCount.Should().Be(9);
            tests.First().Should().Be(mockedTests[0]);
            tests.Last().Should().Be(mockedTests[8]);
        }

        [Fact]
        public void ReturnsFirstPageOrderedAscendingGivenPageSize10AndTotal9Async()
        {
            List<Test> mockedTests = MockTests(9);

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            ISearchQuery pagination = new SearchQuery(0, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.QueryAsync(pagination).GetAwaiter().GetResult();

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

            ISearchQuery pagination = new SearchQuery(0, 10, "TId", SortDirection.Desc);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.Query(pagination);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            tests.Should().NotBeNull().And.HaveCount(9);
            tests.TotalCount.Should().Be(9);
            tests.First().Should().Be(mockedTests[8]);
            tests.Last().Should().Be(mockedTests[0]);
        }

        [Fact]
        public void ReturnsFirstPageOrderedDescendingGivenPageSize10AndTotal9Async()
        {
            List<Test> mockedTests = MockTests(9);

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            ISearchQuery pagination = new SearchQuery(0, 10, "TId", SortDirection.Desc);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.QueryAsync(pagination).GetAwaiter().GetResult();

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
            ISearchQuery pagination = new SearchQuery(0, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.Query(spec, pagination);

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
            ISearchQuery pagination = new SearchQuery(-1, -1);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.Query(spec, pagination);

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
                ISearchQuery pagination = null;
                var testRepository = new TestRepository(mockDbContext.Object);
                testRepository.Query(spec, pagination);
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("pagination");
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenSpecificationAndNullPaginationAsync()
        {
            var mockDbContext = new Mock<DbContext>();

            Action act = () =>
            {
                ISpecification<Test> spec = new DirectSpecification<Test>(t => t.Active);
                ISearchQuery pagination = null;
                var testRepository = new TestRepository(mockDbContext.Object);
                testRepository.QueryAsync(spec, pagination).GetAwaiter().GetResult();
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("pagination");
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenPaginationAndNullSpecification()
        {
            var mockDbContext = new Mock<DbContext>();

            Action act = () =>
            {
                ISpecification<Test> spec = null;
                ISearchQuery pagination = new SearchQuery(0, 10);
                var testRepository = new TestRepository(mockDbContext.Object);
                testRepository.Query(spec, pagination);
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("specification");
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenPaginationAndNullSpecificationAsync()
        {
            var mockDbContext = new Mock<DbContext>();

            Action act = () =>
            {
                ISpecification<Test> spec = null;
                ISearchQuery pagination = new SearchQuery(0, 10);
                var testRepository = new TestRepository(mockDbContext.Object);
                testRepository.QueryAsync(spec, pagination).GetAwaiter().GetResult();
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("specification");
        }

        [Fact]
        public void ReturnsFirstPageOrderedAscendingActivesGivenPageSize10AndTotal100Async()
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
            ISearchQuery pagination = new SearchQuery(0, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.QueryAsync(spec, pagination).GetAwaiter().GetResult();

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
            ISearchQuery pagination = new SearchQuery(0, 10, "TId", SortDirection.Desc);
            var testRepository = new TestRepository(mockDbContext.Object);

            IPagedCollection<Test> tests = testRepository.Query(spec, pagination);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(10);
            tests.TotalCount.Should().Be(98);
            tests.First().Should().Be(mockedTests[98]);
            tests.Last().Should().Be(mockedTests[89]);
        }

        [Fact]
        public void ReturnsFirstPageOrderedDescendingActivesGivenPageSize10AndTotal100Async()
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
            ISearchQuery pagination = new SearchQuery(0, 10, "TId", SortDirection.Desc);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.QueryAsync(spec, pagination).GetAwaiter().GetResult();

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
            ISearchQuery pagination = new SearchQuery(9, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.Query(spec, pagination);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            tests.Should().NotBeNullOrEmpty().And.OnlyContain(x => x.Active, "Any test is not active").And.HaveCount(8);
            tests.TotalCount.Should().Be(98);
            tests.First().Should().Be(mockedTests[91]);
            tests.Last().Should().Be(mockedTests[98]);
        }

        [Fact]
        public void ReturnsLastPageOrderedAscendingActivesGivenPageSize10AndTotal100Async()
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
            ISearchQuery pagination = new SearchQuery(9, 10);
            var testRepository = new TestRepository(mockDbContext.Object);
            IPagedCollection<Test> tests = testRepository.QueryAsync(spec, pagination).GetAwaiter().GetResult();

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
}

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Domain.Aggregates;
using Goal.Infra.Data.Seedwork.Tests.Extensions;
using Goal.Infra.Data.Seedwork.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Seedwork.Tests.Repositories
{
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

            IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);

            var id = Guid.NewGuid();

            Test test = testRepository.Find(id);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            test.Should().NotBeNull();
            test.Id.Should().Be(id);
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

            var id = Guid.NewGuid();

            IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);
            Test test = testRepository.Find(id);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            test.Should().BeNull();
        }

        [Fact]
        public void ReturnsAnEntityGivenIdAsync()
        {
            List<Test> tests = MockTests();

            Mock<DbSet<Test>> mockDbSet = tests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            var id = Guid.NewGuid();

            IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);
            Test test = testRepository.FindAsync(id).GetAwaiter().GetResult();

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            test.Should().NotBeNull();
            test.Id.Should().Be(id);
        }

        [Fact]
        public void ReturnsNullGivenIdAsync()
        {
            List<Test> tests = MockTests();

            Mock<DbSet<Test>> mockDbSet = tests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            var id = Guid.NewGuid();

            IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);
            Test test = testRepository.FindAsync(id).GetAwaiter().GetResult();

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            test.Should().BeNull();
        }

        private static List<Test> MockTests()
        {
            return new List<Test>
            {
                new Test(Guid.NewGuid()),
                new Test(Guid.NewGuid()),
                new Test(Guid.NewGuid()),
                new Test(Guid.NewGuid()),
                new Test(Guid.NewGuid())
            };
        }
    }
}

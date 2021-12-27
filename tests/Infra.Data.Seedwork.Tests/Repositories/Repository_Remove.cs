using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Domain.Aggregates;
using Goal.Infra.Crosscutting.Specifications;
using Goal.Infra.Data.Seedwork.Tests.Extensions;
using Goal.Infra.Data.Seedwork.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Seedwork.Tests.Repositories
{
    public class Repository_Remove
    {
        [Fact]
        public void CallSaveChangesSuccessfullyGivenOneEntity()
        {
            var mockedTests = new List<Test>();

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);
            var test = new Test();
            testRepository.Remove(test);

            mockDbContext.Verify(x => x.Set<Test>().Remove(It.IsAny<Test>()), Times.Once);
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenNullEntity()
        {
            var mockDbContext = new Mock<DbContext>();

            Action act = () =>
            {
                IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);
                testRepository.Remove((Test)null);
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("entity");
        }

        [Fact]
        public void CallSaveChangesSuccessfullyGivenManyEntities()
        {
            var mockedTests = new List<Test>();

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);
            List<Test> tests = MockTests();
            testRepository.Remove(tests);

            mockDbContext.Verify(x => x.Set<Test>().RemoveRange(It.IsAny<IEnumerable<Test>>()), Times.Once);
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenNullEntityEnumerable()
        {
            var mockDbContext = new Mock<DbContext>();

            Action act = () =>
            {
                IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);
                testRepository.Remove((IEnumerable<Test>)null);
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("entities");
        }

        [Fact]
        public void CallSaveChangesSuccessfullyGivenSpecification()
        {
            List<Test> mockedTests = MockTests();

            Mock<DbSet<Test>> mockDbSet = mockedTests
                .AsQueryable()
                .BuildMockDbSet();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            ISpecification<Test> spec = new DirectSpecification<Test>(p => p.Id == 1);
            IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);

            testRepository.Remove(spec);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Exactly(2));
            mockDbContext.Verify(x => x.Set<Test>().RemoveRange(It.IsAny<IEnumerable<Test>>()), Times.Once);
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenNullSpecification()
        {
            var mockDbContext = new Mock<DbContext>();

            Action act = () =>
            {
                IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);
                testRepository.Remove((ISpecification<Test>)null);
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("specification");
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

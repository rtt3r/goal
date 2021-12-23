using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Vantage.Domain;
using Vantage.Infra.Data.Seedwork.Tests.Extensions;
using Vantage.Infra.Data.Seedwork.Tests.Mocks;
using Xunit;

namespace Vantage.Infra.Data.Seedwork.Tests.Repositories
{
    public class Repository_Update
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
            testRepository.Update(test);

            mockDbContext.Verify(x => x.Set<Test>().Update(It.IsAny<Test>()), Times.Once);
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenNullEntity()
        {
            var mockDbContext = new Mock<DbContext>();

            Action act = () =>
            {
                IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);
                testRepository.Update((Test)null);
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
            testRepository.Update(tests);

            mockDbContext.Verify(x => x.Set<Test>().UpdateRange(It.IsAny<IEnumerable<Test>>()), Times.Once);
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenNullEntityEnumerable()
        {
            var mockDbContext = new Mock<DbContext>();

            Action act = () =>
            {
                IEnumerable<Test> tests = null;

                IRepository<Test> testRepository = new TestRepository(mockDbContext.Object);
                testRepository.Update(tests);
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("entities");
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

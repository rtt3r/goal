using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Infra.Data.Auditing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.Auditing
{
    public class AuditChangesInterceptor_SavingChanges
    {
        [Fact]
        public async Task ShouldAddEntriesToAuditAsync()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            var loggerMock = new Mock<ILogger>();

            var interceptorMock = new Mock<TestAuditChangesInterceptor>(
                httpContextAccessorMock.Object,
                loggerMock.Object);

            DbContextOptions dbContextOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: "Test")
                .AddInterceptors(interceptorMock.Object)
                .Options;

            // Act
            using var context = new TestDbContext(dbContextOptions);
            var entity = new TestEntity { Name = "Test" };

            await context.TestEntities.AddAsync(entity);
            await context.SaveChangesAsync();

            // Assert
            interceptorMock.Verify(x =>
                x.SavingChangesAsync(It.IsAny<DbContextEventData>(), It.IsAny<InterceptionResult<int>>(), It.IsAny<CancellationToken>()),
                Times.Once);

            interceptorMock.Verify(x =>
                x.SavedChangesAsync(It.IsAny<SaveChangesCompletedEventData>(), It.IsAny<int>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public void ShouldAddEntriesToAudit()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            var loggerMock = new Mock<ILogger>();

            var interceptorMock = new Mock<TestAuditChangesInterceptor>(
                httpContextAccessorMock.Object,
                loggerMock.Object);

            DbContextOptions dbContextOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: "Test")
                .AddInterceptors(interceptorMock.Object)
                .Options;

            // Act
            using var context = new TestDbContext(dbContextOptions);
            var entity = new TestEntity { Name = "Test" };

            context.TestEntities.Add(entity);
            context.SaveChanges();

            // Assert
            interceptorMock.Verify(x =>
                x.SavingChanges(It.IsAny<DbContextEventData>(), It.IsAny<InterceptionResult<int>>()),
                Times.Once);

            interceptorMock.Verify(x =>
                x.SavedChanges(It.IsAny<SaveChangesCompletedEventData>(), It.IsAny<int>()),
                Times.Once);
        }

        public class TestDbContext : DbContext
        {
            public TestDbContext(DbContextOptions options) : base(options) { }
            public DbSet<Audit> Audits { get; set; }
            public DbSet<TestEntity> TestEntities { get; set; }
        }

        public class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class TestAuditChangesInterceptor : AuditChangesInterceptor
        {
            public TestAuditChangesInterceptor(
                IHttpContextAccessor httpContextAccessor,
                ILogger logger)
                : base(httpContextAccessor, logger)
            { }
        }
    }

}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Seedwork.Infra.Data.Auditing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.Auditing
{
    public class AuditChangesInterceptor_SaveAuditChanges
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ShouldAddEntriesToAudit(bool async)
        {
            // Arrange
            (DbContext context, UniverseAuditChangesInterceptor interceptor) = CreateContext<UniverseAuditChangesInterceptor>();
            DateTimeOffset startedAt = DateTimeOffset.UtcNow;

            // Act
            using DbContext _ = context;

            bool savingEventCalled = false;
            bool saveAuditEventCalled = false;
            int resultFromEvent = 0;
            Audit auditFromEvent = null;
            Exception exceptionFromEvent = null;

            context.SavingChanges += (sender, args) =>
            {
                context.Should().BeSameAs(sender);
                savingEventCalled = true;
            };

            context.SavedChanges += (sender, args) =>
            {
                context.Should().BeSameAs(sender);
                resultFromEvent = args.EntitiesSavedCount;
            };

            context.SaveChangesFailed += (sender, args) =>
            {
                context.Should().BeSameAs(sender);
                exceptionFromEvent = args.Exception;
            };

            interceptor.SaveAudit += (sender, args) =>
            {
                args.Audit.Should().NotBeNull();
                auditFromEvent = args.Audit;
                saveAuditEventCalled = true;
            };

            await context.AddAsync(new Singularity { Id = 35, Type = "Red Dwarf" });

            int savedCount = async
                ? await context.SaveChangesAsync()
                : context.SaveChanges();

            // Assert
            savedCount.Should().Be(1);
            savingEventCalled.Should().BeTrue();
            saveAuditEventCalled.Should().BeTrue();
            savedCount.Should().Be(resultFromEvent);
            exceptionFromEvent.Should().BeNull();

            context.Set<Singularity>().AsNoTracking().Count(e => e.Id == 35).Should().Be(1);

            auditFromEvent.Succeeded.Should().BeTrue();
            auditFromEvent.ErrorMessage.Should().BeNullOrWhiteSpace();
            auditFromEvent.StartTime
                .Should().BeAfter(startedAt)
                .And.BeBefore(auditFromEvent.EndTime);
            auditFromEvent.Entries.Should().HaveCount(1);
            auditFromEvent.Entries.ElementAt(0).Should().NotBeNull();
            auditFromEvent.Entries.ElementAt(0).AuditType.Should().Be("Create");
            auditFromEvent.Entries.ElementAt(0).AuditUser.Should().BeNullOrWhiteSpace();
            auditFromEvent.Entries.ElementAt(0).KeyValues.Should().Be("{\"Id\":35}");
            auditFromEvent.Entries.ElementAt(0).ChangedColumns.Should().BeNullOrWhiteSpace();
            auditFromEvent.Entries.ElementAt(0).OldValues.Should().BeNullOrWhiteSpace();
            auditFromEvent.Entries.ElementAt(0).NewValues.Should().Be("{\"Type\":\"Red Dwarf\"}");
            auditFromEvent.Entries.ElementAt(0).TableName.Should().Be("Singularity");
        }

        private DbContextOptions CreateOptions(IInterceptor interceptor)
        {
            return new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .AddInterceptors(interceptor)
                .Options;
        }

        protected (DbContext, TInterceptor) CreateContext<TInterceptor>()
            where TInterceptor : class, IInterceptor, new()
        {
            var interceptor = new TInterceptor();
            UniverseContext context = CreateContext(interceptor);

            return (context, interceptor);
        }

        public UniverseContext CreateContext(IInterceptor interceptor)
            => new(CreateOptions(interceptor));

        public class UniverseContext : DbContext
        {
            public UniverseContext(DbContextOptions options)
                : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder
                    .Entity<Singularity>()
                    .HasData(
                        new Singularity { Id = 77, Type = "Black Hole" },
                        new Singularity { Id = 88, Type = "Bing Bang" });

                modelBuilder
                    .Entity<Brane>()
                    .HasData(
                        new Brane { Id = 77, Type = "Black Hole?" },
                        new Brane { Id = 88, Type = "Bing Bang?" });
            }
        }

        public class Singularity
        {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }

            public string Type { get; set; }
        }

        public class Brane
        {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }

            public string Type { get; set; }
        }

        public class UniverseAuditChangesInterceptor : AuditChangesInterceptor
        {
            public UniverseAuditChangesInterceptor()
                : base(null, null)
            { }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Seedwork.Infra.Data.Auditing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Tests.Auditing;

public class AuditChangesInterceptor_SaveAuditChanges
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldAuditAddEntries(bool async)
    {
        // Arrange
        (UniverseContext context, UniverseAuditChangesInterceptor interceptor) = CreateContext<UniverseAuditChangesInterceptor>();
        DateTimeOffset startedAt = DateTimeOffset.UtcNow;

        // Act
        using DbContext _ = context;

        bool savingEventCalled = false;
        bool saveAuditEventCalled = false;
        int resultFromEvent = 0;
        Audit? auditFromEvent = null;
        Exception? exceptionFromEvent = null;

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

        int savedCount = 0;

        if (async)
        {
            await context.AddAsync(new Singularity { Id = 35, Type = "Red Dwarf" });
            savedCount = await context.SaveChangesAsync();
        }
        else
        {
            context.Add(new Singularity { Id = 35, Type = "Red Dwarf" });
            savedCount = context.SaveChanges();
        }

        // Assert
        savedCount.Should().Be(1);
        savingEventCalled.Should().BeTrue();
        saveAuditEventCalled.Should().BeTrue();
        savedCount.Should().Be(resultFromEvent);
        exceptionFromEvent.Should().BeNull();

        context.Set<Singularity>().AsNoTracking().Count(e => e.Id == 35).Should().Be(1);

        auditFromEvent?.Id.Should().NotBeNullOrWhiteSpace();
        auditFromEvent?.Succeeded.Should().BeTrue();
        auditFromEvent?.ErrorMessage.Should().BeNullOrWhiteSpace();
        auditFromEvent?.StartTime
            .Should().BeAfter(startedAt)
            .And.BeBefore(auditFromEvent.EndTime);
        auditFromEvent?.Entries.Should().HaveCount(1);
        auditFromEvent?.Entries.ElementAt(0).Should().NotBeNull();
        auditFromEvent?.Entries.ElementAt(0).Id.Should().NotBeNullOrWhiteSpace();
        auditFromEvent?.Entries.ElementAt(0).AuditType.Should().Be("Create");
        auditFromEvent?.Entries.ElementAt(0).AuditUser.Should().BeNullOrWhiteSpace();
        auditFromEvent?.Entries.ElementAt(0).KeyValues.Should().Be("{\"Id\":35}");
        auditFromEvent?.Entries.ElementAt(0).ChangedColumns.Should().BeNullOrWhiteSpace();
        auditFromEvent?.Entries.ElementAt(0).OldValues.Should().BeNullOrWhiteSpace();
        auditFromEvent?.Entries.ElementAt(0).NewValues.Should().Be("{\"Type\":\"Red Dwarf\"}");
        auditFromEvent?.Entries.ElementAt(0).TableName.Should().Be("Singularity");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldAuditAddEntriesWithoutEvent(bool async)
    {
        // Arrange
        (UniverseContext context, UniverseAuditChangesInterceptor interceptor) = CreateContext<UniverseAuditChangesInterceptor>();
        DateTimeOffset startedAt = DateTimeOffset.UtcNow;

        // Act
        using DbContext _ = context;

        bool savingEventCalled = false;
        bool saveAuditEventCalled = false;
        int resultFromEvent = 0;
        Audit? auditFromEvent = null;
        Exception? exceptionFromEvent = null;

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

        int savedCount = 0;

        if (async)
        {
            await context.AddAsync(new Singularity { Id = 35, Type = "Red Dwarf" });
            savedCount = await context.SaveChangesAsync();
        }
        else
        {
            context.Add(new Singularity { Id = 35, Type = "Red Dwarf" });
            savedCount = context.SaveChanges();
        }

        // Assert
        savedCount.Should().Be(1);
        savingEventCalled.Should().BeTrue();
        saveAuditEventCalled.Should().BeFalse();
        savedCount.Should().Be(resultFromEvent);
        auditFromEvent.Should().BeNull();
        exceptionFromEvent.Should().BeNull();

        context.Set<Singularity>().AsNoTracking().Count(e => e.Id == 35).Should().Be(1);

        interceptor.Audit.Id.Should().NotBeNullOrWhiteSpace();
        interceptor.Audit.Succeeded.Should().BeTrue();
        interceptor.Audit.ErrorMessage.Should().BeNullOrWhiteSpace();
        interceptor.Audit.StartTime
            .Should().BeAfter(startedAt)
            .And.BeBefore(interceptor.Audit.EndTime);
        interceptor.Audit.Entries.Should().HaveCount(1);
        interceptor.Audit.Entries.ElementAt(0).Should().NotBeNull();
        interceptor.Audit.Entries.ElementAt(0).Id.Should().NotBeNullOrWhiteSpace();
        interceptor.Audit.Entries.ElementAt(0).AuditType.Should().Be("Create");
        interceptor.Audit.Entries.ElementAt(0).AuditUser.Should().BeNullOrWhiteSpace();
        interceptor.Audit.Entries.ElementAt(0).KeyValues.Should().Be("{\"Id\":35}");
        interceptor.Audit.Entries.ElementAt(0).ChangedColumns.Should().BeNullOrWhiteSpace();
        interceptor.Audit.Entries.ElementAt(0).OldValues.Should().BeNullOrWhiteSpace();
        interceptor.Audit.Entries.ElementAt(0).NewValues.Should().Be("{\"Type\":\"Red Dwarf\"}");
        interceptor.Audit.Entries.ElementAt(0).TableName.Should().Be("Singularity");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldAuditUpdateEntries(bool async)
    {
        // Arrange
        (UniverseContext context, UniverseAuditChangesInterceptor interceptor) = CreateContext<UniverseAuditChangesInterceptor>();
        DateTimeOffset startedAt = DateTimeOffset.UtcNow;

        // Act
        using DbContext _ = context;

        bool savingEventCalled = false;
        bool saveAuditEventCalled = false;
        int resultFromEvent = 0;
        var auditsFromEvent = new List<Audit>();
        Exception? exceptionFromEvent = null;

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
            auditsFromEvent.Add(args.Audit);
            saveAuditEventCalled = true;
        };

        int savedCount = 0;

        var entity = new Singularity { Id = 35, Type = "Red Dwarf" };

        if (async)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            entity.Type = "Red Giant";
            context.Update(entity);
            savedCount = await context.SaveChangesAsync();
        }
        else
        {
            context.Add(entity);
            await context.SaveChangesAsync();

            entity.Type = "Red Giant";
            context.Update(entity);
            savedCount = context.SaveChanges();
        }

        // Assert
        savedCount.Should().Be(1);
        savingEventCalled.Should().BeTrue();
        saveAuditEventCalled.Should().BeTrue();
        savedCount.Should().Be(resultFromEvent);
        exceptionFromEvent.Should().BeNull();

        context.Set<Singularity>().AsNoTracking().Count(e => e.Id == 35).Should().Be(1);

        auditsFromEvent.Should().HaveCount(2);

        auditsFromEvent[0].Id.Should().NotBeNullOrWhiteSpace();
        auditsFromEvent[0].Succeeded.Should().BeTrue();
        auditsFromEvent[0].ErrorMessage.Should().BeNullOrWhiteSpace();
        auditsFromEvent[0].StartTime
            .Should().BeAfter(startedAt)
            .And.BeBefore(auditsFromEvent[0].EndTime);
        auditsFromEvent[0].Entries.Should().HaveCount(1);
        auditsFromEvent[0].Entries.ElementAt(0).Should().NotBeNull();
        auditsFromEvent[0].Entries.ElementAt(0).Id.Should().NotBeNullOrWhiteSpace();
        auditsFromEvent[0].Entries.ElementAt(0).AuditType.Should().Be("Create");
        auditsFromEvent[0].Entries.ElementAt(0).AuditUser.Should().BeNullOrWhiteSpace();
        auditsFromEvent[0].Entries.ElementAt(0).KeyValues.Should().Be("{\"Id\":35}");
        auditsFromEvent[0].Entries.ElementAt(0).ChangedColumns.Should().BeNullOrWhiteSpace();
        auditsFromEvent[0].Entries.ElementAt(0).OldValues.Should().BeNullOrWhiteSpace();
        auditsFromEvent[0].Entries.ElementAt(0).NewValues.Should().Be("{\"Type\":\"Red Dwarf\"}");
        auditsFromEvent[0].Entries.ElementAt(0).TableName.Should().Be("Singularity");

        auditsFromEvent[1].Id.Should().NotBeNullOrWhiteSpace();
        auditsFromEvent[1].Succeeded.Should().BeTrue();
        auditsFromEvent[1].ErrorMessage.Should().BeNullOrWhiteSpace();
        auditsFromEvent[1].StartTime
            .Should().BeAfter(startedAt)
            .And.BeBefore(auditsFromEvent[1].EndTime);
        auditsFromEvent[1].Entries.Should().HaveCount(1);
        auditsFromEvent[1].Entries.ElementAt(0).Should().NotBeNull();
        auditsFromEvent[1].Entries.ElementAt(0).Id.Should().NotBeNullOrWhiteSpace();
        auditsFromEvent[1].Entries.ElementAt(0).AuditType.Should().Be("Update");
        auditsFromEvent[1].Entries.ElementAt(0).AuditUser.Should().BeNullOrWhiteSpace();
        auditsFromEvent[1].Entries.ElementAt(0).KeyValues.Should().Be("{\"Id\":35}");
        auditsFromEvent[1].Entries.ElementAt(0).ChangedColumns.Should().Be("[\"Type\"]");
        auditsFromEvent[1].Entries.ElementAt(0).OldValues.Should().Be("{\"Type\":\"Red Dwarf\"}");
        auditsFromEvent[1].Entries.ElementAt(0).NewValues.Should().Be("{\"Type\":\"Red Giant\"}");
        auditsFromEvent[1].Entries.ElementAt(0).TableName.Should().Be("Singularity");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldAuditDeleteEntries(bool async)
    {
        // Arrange
        (UniverseContext context, UniverseAuditChangesInterceptor interceptor) = CreateContext<UniverseAuditChangesInterceptor>();
        DateTimeOffset startedAt = DateTimeOffset.UtcNow;

        // Act
        using DbContext _ = context;

        bool savingEventCalled = false;
        bool saveAuditEventCalled = false;
        int resultFromEvent = 0;
        var auditsFromEvent = new List<Audit>();
        Exception? exceptionFromEvent = null;

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
            auditsFromEvent.Add(args.Audit);
            saveAuditEventCalled = true;
        };

        int savedCount = 0;

        var entity = new Singularity { Id = 35, Type = "Red Dwarf" };

        if (async)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            context.Remove(entity);
            savedCount = await context.SaveChangesAsync();
        }
        else
        {
            context.Add(entity);
            await context.SaveChangesAsync();

            context.Remove(entity);
            savedCount = context.SaveChanges();
        }

        // Assert
        savedCount.Should().Be(1);
        savingEventCalled.Should().BeTrue();
        saveAuditEventCalled.Should().BeTrue();
        savedCount.Should().Be(resultFromEvent);
        exceptionFromEvent.Should().BeNull();

        context.Set<Singularity>().AsNoTracking().Count(e => e.Id == 35).Should().Be(0);

        auditsFromEvent.Should().HaveCount(2);

        auditsFromEvent[0].Id.Should().NotBeNullOrWhiteSpace();
        auditsFromEvent[0].Succeeded.Should().BeTrue();
        auditsFromEvent[0].ErrorMessage.Should().BeNullOrWhiteSpace();
        auditsFromEvent[0].StartTime
            .Should().BeAfter(startedAt)
            .And.BeBefore(auditsFromEvent[0].EndTime);
        auditsFromEvent[0].Entries.Should().HaveCount(1);
        auditsFromEvent[0].Entries.ElementAt(0).Should().NotBeNull();
        auditsFromEvent[0].Entries.ElementAt(0).Id.Should().NotBeNullOrWhiteSpace();
        auditsFromEvent[0].Entries.ElementAt(0).AuditType.Should().Be("Create");
        auditsFromEvent[0].Entries.ElementAt(0).AuditUser.Should().BeNullOrWhiteSpace();
        auditsFromEvent[0].Entries.ElementAt(0).KeyValues.Should().Be("{\"Id\":35}");
        auditsFromEvent[0].Entries.ElementAt(0).ChangedColumns.Should().BeNullOrWhiteSpace();
        auditsFromEvent[0].Entries.ElementAt(0).OldValues.Should().BeNullOrWhiteSpace();
        auditsFromEvent[0].Entries.ElementAt(0).NewValues.Should().Be("{\"Type\":\"Red Dwarf\"}");
        auditsFromEvent[0].Entries.ElementAt(0).TableName.Should().Be("Singularity");

        auditsFromEvent[0].Id.Should().NotBeNullOrWhiteSpace();
        auditsFromEvent[1].Succeeded.Should().BeTrue();
        auditsFromEvent[1].ErrorMessage.Should().BeNullOrWhiteSpace();
        auditsFromEvent[1].StartTime
            .Should().BeAfter(startedAt)
            .And.BeBefore(auditsFromEvent[1].EndTime);
        auditsFromEvent[1].Entries.Should().HaveCount(1);
        auditsFromEvent[1].Entries.ElementAt(0).Should().NotBeNull();
        auditsFromEvent[0].Entries.ElementAt(0).Id.Should().NotBeNullOrWhiteSpace();
        auditsFromEvent[1].Entries.ElementAt(0).AuditType.Should().Be("Delete");
        auditsFromEvent[1].Entries.ElementAt(0).AuditUser.Should().BeNullOrWhiteSpace();
        auditsFromEvent[1].Entries.ElementAt(0).KeyValues.Should().Be("{\"Id\":35}");
        auditsFromEvent[1].Entries.ElementAt(0).ChangedColumns.Should().BeNullOrWhiteSpace();
        auditsFromEvent[1].Entries.ElementAt(0).OldValues.Should().Be("{\"Type\":\"Red Dwarf\"}");
        auditsFromEvent[1].Entries.ElementAt(0).NewValues.Should().BeNullOrWhiteSpace();
        auditsFromEvent[1].Entries.ElementAt(0).TableName.Should().Be("Singularity");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldThrownAuditAddEntries(bool async)
    {
        // Arrange
        (UniverseContext context, UniverseAuditChangesInterceptor interceptor) = CreateContext<UniverseAuditChangesInterceptor>();
        DateTimeOffset startedAt = DateTimeOffset.UtcNow;

        // Act
        using DbContext _ = context;

        bool savingEventCalled = false;
        bool saveAuditEventCalled = false;
        int resultFromEvent = 0;
        Audit? auditFromEvent = null;
        Exception? exceptionFromEvent = null;

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

        int savedCount = 0;

        Exception? thrown = null;

        try
        {
            context.Update(new Singularity { Id = 35, Type = "Red Dwarf" });

            savedCount = async
                ? await context.SaveChangesAsync()
                : context.SaveChanges();
        }
        catch (Exception e)
        {
            thrown = e;
        }

        // Assert
        savedCount.Should().Be(0);
        savingEventCalled.Should().BeTrue();
        saveAuditEventCalled.Should().BeTrue();
        savedCount.Should().Be(resultFromEvent);
        exceptionFromEvent.Should().BeSameAs(thrown);

        context.Set<Singularity>().AsNoTracking().Count(e => e.Id == 35).Should().Be(0);

        auditFromEvent?.Id.Should().NotBeNullOrWhiteSpace();
        auditFromEvent?.Succeeded.Should().BeFalse();
        auditFromEvent?.ErrorMessage.Should().Be(exceptionFromEvent?.Message);
        auditFromEvent?.StartTime
            .Should().BeAfter(startedAt)
            .And.BeBefore(auditFromEvent.EndTime);
        auditFromEvent?.Entries.Should().HaveCount(1);
        auditFromEvent?.Entries.ElementAt(0).Should().NotBeNull();
        auditFromEvent?.Entries.ElementAt(0).Id.Should().NotBeNullOrWhiteSpace();
        auditFromEvent?.Entries.ElementAt(0).AuditType.Should().Be("None");
        auditFromEvent?.Entries.ElementAt(0).AuditUser.Should().BeNullOrWhiteSpace();
        auditFromEvent?.Entries.ElementAt(0).KeyValues.Should().Be("{\"Id\":35}");
        auditFromEvent?.Entries.ElementAt(0).ChangedColumns.Should().BeNullOrWhiteSpace();
        auditFromEvent?.Entries.ElementAt(0).OldValues.Should().BeNullOrWhiteSpace();
        auditFromEvent?.Entries.ElementAt(0).NewValues.Should().BeNullOrWhiteSpace();
        auditFromEvent?.Entries.ElementAt(0).TableName.Should().Be("Singularity");
    }

    private static DbContextOptions CreateOptions(IInterceptor interceptor)
    {
        return new DbContextOptionsBuilder()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .AddInterceptors(interceptor)
            .Options;
    }

    protected static (UniverseContext, TInterceptor) CreateContext<TInterceptor>()
        where TInterceptor : class, IAuditChangesInterceptor, new()
    {
        var interceptor = new TInterceptor();
        (UniverseContext context, IAuditChangesInterceptor _) = CreateContext(interceptor);

        return (context, interceptor);
    }

    public static (UniverseContext, IAuditChangesInterceptor) CreateContext(IAuditChangesInterceptor interceptor)
    {
        UniverseContext context = new(CreateOptions(interceptor));
        return (context, interceptor);
    }

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

        public string Type { get; set; } = null!;
    }

    public class Brane
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Type { get; set; } = null!;
    }

    public class UniverseAuditChangesInterceptor : AuditChangesInterceptor
    {
        public Audit Audit { get; private set; } = null!;

        protected override void SaveAuditChanges(Audit audit)
            => Audit = audit;
    }
}

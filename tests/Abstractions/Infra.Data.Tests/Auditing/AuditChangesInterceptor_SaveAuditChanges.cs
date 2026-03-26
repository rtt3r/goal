using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using FluentAssertions;
using Goal.Infra.Data.Auditing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Goal.Infra.Data.Tests.Auditing;

public class AuditChangesInterceptor_SaveAuditChanges
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldAuditAddEntries(bool async)
    {
        (UniverseContext context, _) = CreateContext();
        using UniverseContext _db = context;

        if (async)
        {
            await context.AddAsync(new Singularity { Id = 35, Type = "Red Dwarf" });
            await context.SaveChangesAsync();
        }
        else
        {
            context.Add(new Singularity { Id = 35, Type = "Red Dwarf" });
            context.SaveChanges();
        }

        AuditLog audit = GetAudits(context).Should().ContainSingle().Subject;

        audit.ChangeType.Should().Be(nameof(EntityState.Added));
        audit.EntityName.Should().Be(nameof(Singularity));
        audit.ChangedBy.Should().BeNullOrWhiteSpace();
        AssertJsonKeyValues(audit.KeyValues!, ("Id", JsonValueKind.Number, "35"));
        audit.OldValues.Should().BeNull();
        AssertJsonSnapshot(audit.NewValues!, ("Id", 35), ("Type", "Red Dwarf"));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldAuditUpdateEntries(bool async)
    {
        (UniverseContext context, _) = CreateContext();
        using UniverseContext _db = context;

        var entity = new Singularity { Id = 35, Type = "Red Dwarf" };

        if (async)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            entity.Type = "Red Giant";
            context.Update(entity);
            await context.SaveChangesAsync();
        }
        else
        {
            context.Add(entity);
            context.SaveChanges();

            entity.Type = "Red Giant";
            context.Update(entity);
            context.SaveChanges();
        }

        List<AuditLog> audits = GetAudits(context);
        audits.Should().HaveCount(2);

        audits[0].ChangeType.Should().Be(nameof(EntityState.Added));
        audits[0].EntityName.Should().Be(nameof(Singularity));
        audits[0].OldValues.Should().BeNull();

        AssertJsonSnapshot(audits[0].NewValues!, ("Id", 35), ("Type", "Red Dwarf"));

        audits[1].ChangeType.Should().Be(nameof(EntityState.Modified));
        audits[1].EntityName.Should().Be(nameof(Singularity));

        AssertJsonKeyValues(audits[1].KeyValues!, ("Id", JsonValueKind.Number, "35"));
        AssertJsonSnapshot(audits[1].OldValues!, ("Type", "Red Dwarf"));
        AssertJsonSnapshot(audits[1].NewValues!, ("Type", "Red Giant"));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldAuditDeleteEntries(bool async)
    {
        (UniverseContext context, _) = CreateContext();
        using UniverseContext _db = context;

        var entity = new Singularity { Id = 35, Type = "Red Dwarf" };

        if (async)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
        else
        {
            context.Add(entity);
            context.SaveChanges();
            context.Remove(entity);
            context.SaveChanges();
        }

        context.Set<Singularity>().AsNoTracking().Any(e => e.Id == 35).Should().BeFalse();

        List<AuditLog> audits = GetAudits(context);
        audits.Should().HaveCount(2);

        audits[0].ChangeType.Should().Be(nameof(EntityState.Added));
        audits[1].ChangeType.Should().Be(nameof(EntityState.Deleted));
        audits[1].EntityName.Should().Be(nameof(Singularity));
        audits[1].NewValues.Should().BeNull();

        AssertJsonKeyValues(audits[1].KeyValues!, ("Id", JsonValueKind.Number, "35"));
        AssertJsonSnapshot(audits[1].OldValues!, ("Id", 35), ("Type", "Red Dwarf"));
    }

    private static void AssertJsonKeyValues(string json, params (string Key, JsonValueKind Kind, string Raw)[] expected)
    {
        using var doc = JsonDocument.Parse(json);

        foreach ((string? key, JsonValueKind kind, string? raw) in expected)
        {
            doc.RootElement.TryGetProperty(key, out JsonElement prop).Should().BeTrue();
            prop.ValueKind.Should().Be(kind);
            prop.GetRawText().Should().Be(raw);
        }
    }

    private static void AssertJsonSnapshot(string json, params (string Key, object Value)[] pairs)
    {
        using var doc = JsonDocument.Parse(json);
        foreach ((string? key, object? value) in pairs)
        {
            doc.RootElement.TryGetProperty(key, out JsonElement prop).Should().BeTrue();
            switch (value)
            {
                case int i:
                    prop.GetInt32().Should().Be(i);
                    break;
                case string s:
                    prop.GetString().Should().Be(s);
                    break;
                default:
                    throw new ArgumentException(nameof(value));
            }
        }

        doc.RootElement.EnumerateObject().Count().Should().Be(pairs.Length);
    }

    private static DbContextOptions CreateOptions(IInterceptor interceptor)
    {
        return new DbContextOptionsBuilder()
            .UseInMemoryDatabase(databaseName: Guid.CreateVersion7().ToString())
            .AddInterceptors(interceptor)
            .Options;
    }

    private static (UniverseContext Context, UniverseAuditChangesInterceptor Interceptor) CreateContext()
    {
        var interceptor = new UniverseAuditChangesInterceptor();
        UniverseContext context = new(CreateOptions(interceptor));
        return (context, interceptor);
    }

    private static List<AuditLog> GetAudits(UniverseContext context)
        => context.Set<AuditLog>().AsNoTracking()
            .OrderBy(a => a.Timestamp)
            .ThenBy(a => a.Id)
            .ToList();

    public class UniverseContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLog>();

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
        public override string GetCurrentUserId() => string.Empty;
    }
}

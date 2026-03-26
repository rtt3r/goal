namespace Goal.Infra.Data.Auditing;

public class AuditLog<TKey>
{
    public TKey Id { get; set; } = default!;
    public required string ChangeType { get; set; }
    public required string EntityName { get; set; }
    public required string KeyValues { get; set; }
    public string? ChangedBy { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}

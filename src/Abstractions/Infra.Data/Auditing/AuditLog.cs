namespace Goal.Infra.Data.Auditing;

public class AuditLog : AuditLog<string>
{
    internal AuditLog()
    {
        Id = Guid.CreateVersion7().ToString();
    }
}

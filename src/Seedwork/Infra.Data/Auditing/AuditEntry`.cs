using System;

namespace Goal.Seedwork.Infra.Data.Auditing;

public class AuditEntry : AuditEntry<string>
{
    public AuditEntry()
    {
        Id = Guid.NewGuid().ToString();
    }
}

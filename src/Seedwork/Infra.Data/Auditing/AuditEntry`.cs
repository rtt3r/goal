using System;

namespace Goal.Infra.Data.Auditing;

public class AuditEntry : AuditEntry<string>
{
    public AuditEntry()
    {
        Id = Guid.NewGuid().ToString();
    }
}

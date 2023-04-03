using System;
using System.Collections.Generic;

namespace Goal.Seedwork.Infra.Data.Auditing;

public class Audit<TKey>
{
    public TKey Id { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public bool Succeeded { get; set; }
    public string ErrorMessage { get; set; }
    public ICollection<AuditEntry<TKey>> Entries { get; } = new List<AuditEntry<TKey>>();
}

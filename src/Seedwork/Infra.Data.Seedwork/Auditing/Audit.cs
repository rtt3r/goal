using System;
using System.Collections.Generic;

namespace Goal.Infra.Data.Seedwork.Auditing
{
    public class Audit
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public ICollection<AuditEntry> Entries { get; } = new List<AuditEntry>();
    }
}

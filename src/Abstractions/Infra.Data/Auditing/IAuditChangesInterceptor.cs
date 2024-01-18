using System;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Goal.Infra.Data.Auditing;

public interface IAuditChangesInterceptor : ISaveChangesInterceptor
{
    event EventHandler<AuditChangesInterceptor.SaveAuditEventArgs> SaveAudit;
}
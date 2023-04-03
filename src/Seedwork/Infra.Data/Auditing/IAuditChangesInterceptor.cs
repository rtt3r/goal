using System;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Goal.Seedwork.Infra.Data.Auditing;

public interface IAuditChangesInterceptor : ISaveChangesInterceptor
{
    event EventHandler<AuditChangesInterceptor.SaveAuditEventArgs> SaveAudit;
}
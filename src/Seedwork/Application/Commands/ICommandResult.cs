using System.Collections.Generic;
using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Goal.Application.Commands;

public interface ICommandResult
{
    bool IsSucceeded { get; }
    IEnumerable<Notification> Notifications { get; }
    bool HasDomainViolation { get; }
    bool HasExternalError { get; }
    bool HasInternalError { get; }
    bool HasInformation { get; }
    bool HasInputValidation { get; }
    bool HasResourceNotFound { get; }
}

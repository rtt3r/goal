using System.Collections.Generic;
using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Goal.Seedwork.Application.Commands
{
    public interface ICommandResult
    {
        bool IsSucceeded { get; }
        IEnumerable<Notification> Notifications { get; }
        bool HasDomainViolation();
        bool HasExternalError();
        bool HasInternalError();
        bool HasInformation();
        bool HasInputValidation();
    }
}

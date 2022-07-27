using System.Collections.Generic;
using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Goal.Seedwork.Domain.Commands
{
    public interface ICommandResult
    {
        bool IsSucceeded { get; }
        bool HasInformation { get; }
        bool HasInputValidation { get; }
        bool HasDomainViolation { get; }
        bool HasInternalError { get; }
        bool HasExternalError { get; }
        ICollection<INotification> Notifications { get; }
    }
}

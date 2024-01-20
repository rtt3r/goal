using System.Collections.Generic;
using Goal.Infra.Crosscutting.Notifications;

namespace Goal.Application.Commands;

public record CommandResult<T>(bool IsSucceeded, T? Data, IEnumerable<Notification> Notifications)
    : CommandResult(IsSucceeded, Notifications), ICommandResult<T>
{
}

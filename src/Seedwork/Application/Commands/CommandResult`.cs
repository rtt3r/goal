using System.Collections.Generic;
using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Goal.Application.Abstractions.Commands;

public record CommandResult<T>(bool IsSucceeded, T? Data, IEnumerable<Notification> Notifications)
    : CommandResult(IsSucceeded, Notifications), ICommandResult<T>
{
}

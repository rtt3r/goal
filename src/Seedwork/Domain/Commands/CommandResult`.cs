using System.Collections.Generic;
using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Goal.Seedwork.Domain.Commands
{
    public class CommandResult<T> : CommandResult, ICommandResult<T>
    {
        internal CommandResult(bool isSucceeded, T data, ICollection<INotification> notifications)
            : base(isSucceeded, notifications)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}

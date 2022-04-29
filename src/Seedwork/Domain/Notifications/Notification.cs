using MediatR;

namespace Goal.Seedwork.Domain.Notifications
{
    public class Notification : INotification
    {
        public string Type { get; }
        public string Code { get; private set; }
        public string Message { get; private set; }

        public Notification(string message)
        {
            Type = GetType().Name;
            Message = message;
        }

        public Notification(string code, string message)
            : this(message)
        {
            Code = code;
        }
    }
}

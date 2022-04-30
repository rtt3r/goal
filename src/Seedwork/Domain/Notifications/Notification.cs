using System;
using MediatR;

namespace Goal.Seedwork.Domain.Notifications
{
    public class Notification : INotification
    {
        public NotificationType Type { get; private set; }
        public string Code { get; private set; }
        public string Message { get; private set; }
        public string ParamName { get; private set; }

        private Notification(NotificationType type, string code, string message)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException($"'{nameof(code)}' cannot be null or empty.", nameof(code));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException($"'{nameof(message)}' cannot be null or empty.", nameof(message));
            }

            Type = type;
            Code = code;
            Message = message;
        }

        private Notification(NotificationType type, string code, string message, string paramName)
            : this(type, code, message)
        {
            if (string.IsNullOrWhiteSpace(paramName))
            {
                throw new ArgumentException($"'{nameof(paramName)}' cannot be null or empty.", nameof(paramName));
            }

            ParamName = paramName;
        }

        public static Notification Info(string code, string message)
            => new Notification(NotificationType.Info, code, message);
        public static Notification Fail(string code, string message)
            => new Notification(NotificationType.Fail, code, message);
        public static Notification Violation(string code, string message)
            => new Notification(NotificationType.Violation, code, message);
        public static Notification Violation(string code, string message, string paramName)
            => new Notification(NotificationType.Violation, code, message, paramName);
    }
}

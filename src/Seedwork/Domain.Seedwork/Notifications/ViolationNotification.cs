namespace Goal.Domain.Seedwork.Notifications
{
    public sealed class ViolationNotification : Notification
    {
        public string PropertyName { get; }

        public ViolationNotification(string propertyName, string message)
            : this(null, propertyName, message)
        {
        }

        public ViolationNotification(string code, string propertyName, string message)
            : base(code, message)
        {
        }
    }
}

namespace Goal.Domain.Seedwork.Notifications
{
    public class ValidationNotification : Notification
    {
        public ValidationNotification(string key, string value)
            : base(key, value)
        {
        }
    }
}

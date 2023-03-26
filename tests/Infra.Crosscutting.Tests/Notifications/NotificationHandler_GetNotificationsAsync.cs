using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Notifications
{
    public class NotificationHandler_GetNotificationsAsync
    {
        [Fact]
        public async Task ReturnsAllNotifications()
        {
            // Arrange
            var handler = new TestNotificationHandler();
            Notification[] notifications = new[]
            {
                Notification.Information("test1", "Test message 1"),
                Notification.InternalError("test2", "Test message 2")
            };

            foreach (Notification notification in notifications)
            {
                await handler.HandleAsync(notification);
            }

            // Act
            IEnumerable<Notification> result = await handler.GetNotificationsAsync();

            // Assert
            result.Should().BeEquivalentTo(notifications);
        }

        private class TestNotificationHandler : NotificationHandler<Notification>
        {
        }
    }
}
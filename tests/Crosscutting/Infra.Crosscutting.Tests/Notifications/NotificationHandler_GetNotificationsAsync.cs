using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Notifications;

public class NotificationHandler_GetNotificationsAsync
{
    [Fact]
    public async Task ReturnsAllNotifications()
    {
        // Arrange
        var handler = new TestNotificationHandler();
        Notification[] notifications =
        [
            Notification.Information("test1", "Test message 1"),
            Notification.InternalError("test2", "Test message 2")
        ];

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
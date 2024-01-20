using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Notifications;

public class NotificationHandler_HandleAsync
{
    [Fact]
    public async Task AddsNotification()
    {
        // Arrange
        var handler = new TestNotificationHandler();
        var notification = Notification.Information("test", "Test message");

        // Act
        await handler.HandleAsync(notification);

        // Assert
        handler.GetNotifications().Should().HaveCount(1);
    }

    private class TestNotificationHandler : NotificationHandler<Notification>
    {
        public override IEnumerable<Notification> GetNotifications()
        => base.GetNotifications();
    }
}
using FluentAssertions;
using Goal.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Notifications;

public class Notification_ResourceNotFound
{
    [Fact]
    public void ShouldSetTypeCodeAndMessage_WhenCalled()
    {
        // Arrange & Act
        var notification = Notification.ResourceNotFound("I001", "Invalid input");

        //Assert
        notification.Type.Should().Be(NotificationType.ResourceNotFound);
        notification.Code.Should().Be("I001");
        notification.Message.Should().Be("Invalid input");
    }
}
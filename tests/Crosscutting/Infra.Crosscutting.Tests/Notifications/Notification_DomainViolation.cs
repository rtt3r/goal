using FluentAssertions;
using Goal.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Notifications;

public class Notification_DomainViolation
{
    [Fact]
    public void ShouldSetTypeCodeAndMessage_WhenCalled()
    {
        // Arrange & Act
        var notification = Notification.DomainViolation("I001", "Invalid input");

        //Assert
        notification.Type.Should().Be(NotificationType.DomainViolation);
        notification.Code.Should().Be("I001");
        notification.Message.Should().Be("Invalid input");
    }
}
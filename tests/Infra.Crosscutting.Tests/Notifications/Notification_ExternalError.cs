using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Notifications
{
    public class Notification_ExternalError
    {
        [Fact]
        public void ShouldSetTypeCodeAndMessage_WhenCalled()
        {
            // Arrange & Act
            var notification = Notification.ExternalError("I001", "Invalid input");

            //Assert
            notification.Type.Should().Be(NotificationType.ExternalError);
            notification.Code.Should().Be("I001");
            notification.Message.Should().Be("Invalid input");
        }
    }
}
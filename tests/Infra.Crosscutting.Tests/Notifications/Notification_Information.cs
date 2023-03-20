using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Notifications
{
    public class Notification_Information
    {
        [Fact]
        public void ShouldCreateNotification_WithTypeCodeAndMessageInformation()
        {
            // Arrange, Act, & Assert
            Notification.Information("I001", "Information")
                .Type.Should().Be(NotificationType.Information);


            // Arrange & Act
            var notification = Notification.Information("I001", "Information");

            //Assert
            notification.Type.Should().Be(NotificationType.Information);
            notification.Code.Should().Be("I001");
            notification.Message.Should().Be("Information");
        }
    }
}
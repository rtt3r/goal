using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Notifications
{
    public class Notification_InputValidation
    {
        [Fact]
        public void ShouldSetTypeCodeMessageAndParamName_WhenCalled()
        {
            // Arrange & Act
            var notification = Notification.InputValidation("I001", "Invalid input", "ParamName");

            //Assert
            notification.Type.Should().Be(NotificationType.InputValidation);
            notification.Code.Should().Be("I001");
            notification.Message.Should().Be("Invalid input");
            notification.ParamName.Should().Be("ParamName");
        }
    }
}
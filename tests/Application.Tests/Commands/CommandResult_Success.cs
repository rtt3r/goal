using System;
using FluentAssertions;
using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Seedwork.Application.Tests.Commands
{
    public class CommandResult_Success
    {
        [Fact]
        public void WithValidParams_ShouldReturnSuccessCommandResult()
        {
            // Arrange
            Notification[] notifications = new[] { Notification.Information("01", "Notification message") };

            // Act
            ICommandResult result = CommandResult.Success(notifications);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceeded.Should().BeTrue();
            result.Notifications.Should().BeEquivalentTo(notifications);
        }

        [Fact]
        public void WithInvalidNotificationType_ShouldThrowInvalidOperationException()
        {
            // Arrange
            Notification[] notifications = new[] { Notification.DomainViolation("01", "Notification message") };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => CommandResult.Success(notifications));
        }

        [Fact]
        public void WithValidParamsAndData_ShouldReturnSuccessCommandResultWithGenericData()
        {
            // Arrange
            int data = 42;
            Notification[] notifications = new[] { Notification.Information("01", "Notification message") };

            // Act
            ICommandResult<int> result = CommandResult.Success(data, notifications);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceeded.Should().BeTrue();
            result.Data.Should().Be(data);
            result.Notifications.Should().BeEquivalentTo(notifications);
        }

        [Fact]
        public void WithInvalidNotificationTypeAndData_ShouldThrowInvalidOperationException()
        {
            // Arrange
            int data = 42;
            Notification[] notifications = new[] { Notification.DomainViolation("01", "Notification message") };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => CommandResult.Success(data, notifications));
        }
    }

    public class CommandResult_Failure
    {
        [Fact]
        public void WithValidParams_ShouldReturnFailureCommandResult()
        {
            // Arrange
            Notification[] notifications = new[] { Notification.DomainViolation("01", "Notification message") };

            // Act
            ICommandResult result = CommandResult.Failure(notifications);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceeded.Should().BeFalse();
            result.Notifications.Should().BeEquivalentTo(notifications);
        }

        [Fact]
        public void WithInvalidNotificationType_ShouldThrowInvalidOperationException()
        {
            // Arrange
            Notification[] notifications = new[] { Notification.Information("01", "Notification message") };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => CommandResult.Failure(notifications));
        }

        [Fact]
        public void WithValidParamsAndData_ShouldReturnFailureCommandResultWithGenericData()
        {
            // Arrange
            int data = 42;
            Notification[] notifications = new[] { Notification.DomainViolation("01", "Notification message") };

            // Act
            ICommandResult<int> result = CommandResult.Failure(data, notifications);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceeded.Should().BeFalse();
            result.Data.Should().Be(data);
            result.Notifications.Should().BeEquivalentTo(notifications);
        }

        [Fact]
        public void WithInvalidNotificationTypeAndData_ShouldThrowInvalidOperationException()
        {
            // Arrange
            int data = 42;
            Notification[] notifications = new[] { Notification.Information("01", "Notification message") };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => CommandResult.Failure(data, notifications));
        }
    }
}

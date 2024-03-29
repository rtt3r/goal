using System;
using FluentAssertions;
using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Application.Tests.Commands;

public class CommandResult_Success
{
    [Fact]
    public void WithValidParams_ShouldReturnSuccessCommandResult()
    {
        // Arrange
        Notification[] notifications = [Notification.Information("01", "Notification message")];

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
        Notification[] notifications = [Notification.DomainViolation("01", "Notification message")];

        // Act
        Func<ICommandResult> act = () => CommandResult.Success(notifications);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("For 'Success' result only notifications of type 'Information' are accepted.");
    }

    [Fact]
    public void WithValidParamsAndData_ShouldReturnSuccessCommandResultWithGenericData()
    {
        // Arrange
        int data = 42;
        Notification[] notifications = [Notification.Information("01", "Notification message")];

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
        Notification[] notifications = [Notification.DomainViolation("01", "Notification message")];

        // Act
        Func<ICommandResult<int>> act = () => CommandResult.Success(data, notifications);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("For 'Success' result only notifications of type 'Information' are accepted.");
    }
}

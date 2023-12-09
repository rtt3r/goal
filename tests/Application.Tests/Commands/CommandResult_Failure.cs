using System;
using FluentAssertions;
using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Seedwork.Application.Tests.Commands;

public class CommandResult_Failure
{
    [Fact]
    public void WithValidParams_ShouldReturnFailureCommandResult()
    {
        // Arrange
        Notification[] notifications = [Notification.DomainViolation("01", "Notification message")];

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
        Notification[] notifications = [Notification.Information("01", "Notification message")];

        // Act
        Func<ICommandResult> act = () => CommandResult.Failure(notifications);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("For 'Failure' result it's necessary to report failure notifications.");
    }

    [Fact]
    public void WithValidParamsAndData_ShouldReturnFailureCommandResultWithGenericData()
    {
        // Arrange
        int data = 42;
        Notification[] notifications = [Notification.DomainViolation("01", "Notification message")];

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
        Notification[] notifications = [Notification.Information("01", "Notification message")];

        // Act
        Func<ICommandResult<int>> act = () => CommandResult.Failure(data, notifications);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("For 'Failure' result it's necessary to report failure notifications.");
    }

    [Fact]
    public void WithEmptyNotificationsAndData_ShouldThrowInvalidOperationException()
    {
        // Arrange
        int data = 42;
        Notification[] notifications = Array.Empty<Notification>();

        // Act
        Func<ICommandResult<int>> act = () => CommandResult.Failure(data, notifications);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("For 'Failure' result it's necessary to report failure notifications.");
    }

    [Fact]
    public void WithEmptyNotifications_ShouldThrowInvalidOperationException()
    {
        // Arrange
        Notification[] notifications = Array.Empty<Notification>();

        // Act
        Func<ICommandResult> act = () => CommandResult.Failure(notifications);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("For 'Failure' result it's necessary to report failure notifications.");
    }
}

using System.Collections.Generic;
using FluentAssertions;
using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Seedwork.Application.Tests.Commands;

public class CommandResult_HasInformation
{
    [Fact]
    public void WithNoNotifications_ReturnsFalse()
    {
        // Arrange
        ICommandResult commandResult = CommandResult.Success();

        // Act
        bool result = commandResult.HasInformation;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void WithNonInformationNotifications_ReturnsFalse()
    {
        // Arrange
        var notifications = new List<Notification>
        {
            Notification.DomainViolation("01", "An error occurred"),
            Notification.InternalError("02", "Another error occurred")
        };
        ICommandResult commandResult = CommandResult.Failure(notifications);

        // Act
        bool result = commandResult.HasInformation;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void WithInformationNotifications_ReturnsTrue()
    {
        // Arrange
        var notifications = new List<Notification>
        {
            Notification.Information("01", "A message"),
            Notification.InputValidation("02", "Invalid input", "test")
        };
        ICommandResult commandResult = CommandResult.Failure(notifications);

        // Act
        bool result = commandResult.HasInformation;

        // Assert
        result.Should().BeTrue();
    }
}
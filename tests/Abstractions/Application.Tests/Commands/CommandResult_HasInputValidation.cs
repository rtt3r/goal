using System.Collections.Generic;
using FluentAssertions;
using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Application.Tests.Commands;

public class CommandResult_HasInputValidation
{
    [Fact]
    public void Default_Instance_Should_Have_Empty_Notifications_List()
    {
        // Act
        ICommandResult commandResult = CommandResult.Success();

        // Assert
        commandResult.Notifications.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_True_If_Any_Input_Validation_Notification_Is_Present()
    {
        // Arrange
        var notifications = new List<Notification>
        {
            Notification.Information("01", "Information notification."),
            Notification.InputValidation("03", "Input validation notification.", "tests"),
            Notification.DomainViolation("02", "Domain violation notification.")
        };

        ICommandResult commandResult = CommandResult.Failure(notifications);

        // Act
        bool hasInputValidation = commandResult.HasInputValidation;

        // Assert
        hasInputValidation.Should().BeTrue();
    }

    [Fact]
    public void HasExternalError_Should_Return_True_If_Any_External_Error_Notification_Is_Present()
    {
        // Arrange
        var notifications = new List<Notification>
        {
            Notification.InternalError("01", "Internal error notification."),
            Notification.ExternalError("02", "External error notification."),
            Notification.Information("03", "Information notification.")
        };
        ICommandResult commandResult = CommandResult.Failure(notifications);

        // Act
        bool hasExternalError = commandResult.HasExternalError;

        // Assert
        hasExternalError.Should().BeTrue();
    }

    [Fact]
    public void HasInternalError_Should_Return_True_If_Any_Internal_Error_Notification_Is_Present()
    {
        // Arrange
        var notifications = new List<Notification>
        {
            Notification.Information("01", "Information notification."),
            Notification.InternalError("02", "Internal error notification."),
            Notification.InputValidation("03", "Input validation notification.", "test")
        };
        ICommandResult commandResult = CommandResult.Failure(notifications);

        // Act
        bool hasInternalError = commandResult.HasInternalError;

        // Assert
        hasInternalError.Should().BeTrue();
    }

    [Fact]
    public void HasInputValidation_Should_Return_True_If_Any_Input_Validation_Notification_Is_Present()
    {
        // Arrange
        var notifications = new List<Notification>
        {
            Notification.ResourceNotFound("01", "Resource not found notification."),
            Notification.Information("02", "Information notification."),
            Notification.InputValidation("03", "Input validation notification.", "test")
        };
        ICommandResult commandResult = CommandResult.Failure(notifications);

        // Act
        bool hasInputValidation = commandResult.HasInputValidation;

        // Assert
        hasInputValidation.Should().BeTrue();
    }

    [Fact]
    public void HasResourceNotFound_Should_Return_True_If_Any_Resource_Not_Found_Notification_Is_Present()
    {
        // Arrange
        var notifications = new List<Notification>
        {
            Notification.ExternalError("01", "External error notification."),
            Notification.ResourceNotFound("02", "Resource not found notification."),
            Notification.InternalError("03", "Internal error notification.")
        };
        ICommandResult commandResult = CommandResult.Failure(notifications);

        // Act
        bool hasResourceNotFound = commandResult.HasResourceNotFound;

        // Assert
        hasResourceNotFound.Should().BeTrue();
    }

    [Fact]
    public void HasInformation_Should_Return_True_If_Any_Information_Notification_Is_Present()
    {
        // Arrange
        var notifications = new List<Notification>
        {
            Notification.Information("01", "Information notification."),
            Notification.DomainViolation("03", "Domain violation notification."),
            Notification.InputValidation("02", "Input validation notification.", "test")
        };
        ICommandResult commandResult = CommandResult.Failure(notifications);

        // Act
        bool hasInformation = commandResult.HasInformation;

        // Assert
        hasInformation.Should().BeTrue();
    }

    [Fact]
    public void Should_Return_False_If_No_Input_Validation_Notification_Is_Present()
    {
        // Arrange
        var notifications = new List<Notification>
        {
            Notification.Information("01", "Information notification."),
            Notification.DomainViolation("02", "Domain violation notification."),
            Notification.ExternalError("03", "External error notification.")
        };
        ICommandResult commandResult = CommandResult.Failure(notifications);

        // Act
        bool hasInputValidation = commandResult.HasInputValidation;

        // Assert
        hasInputValidation.Should().BeFalse();
    }

    [Fact]
    public void WhenNotificationsContainInputValidation_ReturnsTrue()
    {
        // Arrange
        var notifications = new List<Notification>
        {
            Notification.InputValidation("01", "There was a input validation", "tests")
        };
        ICommandResult commandResult = CommandResult.Failure(notifications);

        // Act
        bool result = commandResult.HasInputValidation;

        // Assert
        result.Should().BeTrue();
    }
}

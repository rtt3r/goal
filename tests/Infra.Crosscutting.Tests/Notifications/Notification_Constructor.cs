using System;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Notifications;

public class Notification_Constructor
{
    [Fact]
    public void ShouldThrowArgumentException_WhenCodeIsEmptyOrNull()
    {
        // Arrange, Act, & Assert
        FluentActions.Invoking(() => Notification.Information("", "Info"))
            .Should().Throw<ArgumentException>().WithMessage("'code' cannot be null or empty. (Parameter 'code')");
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenMessageIsEmptyOrNull()
    {
        // Arrange, Act, & Assert
        FluentActions.Invoking(() => Notification.Information("I001", ""))
            .Should().Throw<ArgumentException>().WithMessage("'message' cannot be null or empty. (Parameter 'message')");
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenParamNameIsEmptyOrNull()
    {
        // Arrange, Act, & Assert
        FluentActions.Invoking(() => Notification.InputValidation("I001", "InputValidation", ""))
            .Should().Throw<ArgumentException>().WithMessage("'paramName' cannot be null or empty. (Parameter 'paramName')");
    }
}
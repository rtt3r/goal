using System;
using FluentAssertions;
using Goal.Infra.Crosscutting.Notifications;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Notifications;

public class Notification_Constructor
{
    [Fact]
    public void ShouldThrowArgumentException_WhenCodeIsEmptyOrNull()
    {
        // Arrange, Act, & Assert
        FluentActions.Invoking(() => Notification.Information("", "Info"))
            .Should().Throw<ArgumentException>().WithMessage("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'code')");
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenMessageIsEmptyOrNull()
    {
        // Arrange, Act, & Assert
        FluentActions.Invoking(() => Notification.Information("I001", ""))
            .Should().Throw<ArgumentException>().WithMessage("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'message')");
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenParamNameIsEmptyOrNull()
    {
        // Arrange, Act, & Assert
        FluentActions.Invoking(() => Notification.InputValidation("I001", "InputValidation", ""))
            .Should().Throw<ArgumentException>().WithMessage("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'paramName')");
    }
}
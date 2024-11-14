using System.Globalization;
using FluentAssertions;
using Goal.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Trying;

public class Option_Unit
{
    [Fact]
    public void ShouldReturnNonNullUnit()
    {
        // Act
        UnitType result = Option.Unit();

        // Assert
        result.Should().NotBeNull();
    }
}

public class Try_Of
{
    [Fact]
    public void ReturnsSuccessfulTry_WhenGivenSuccess()
    {
        // Arrange
        string message = "Something went wrong.";

        // Act
        var result = Try<string, int>.Of(message);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.GetSuccess().Should().Be(message);
    }

    [Fact]
    public void ReturnsFailedTry_WhenGivenFailure()
    {
        // Arrange
        int value = 42;

        // Act
        var result = Try<string, int>.Of(value);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.GetFailure().Should().Be(value);
    }
}
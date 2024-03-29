using System;
using FluentAssertions;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Ensuring;

public class Ensure_Argument_Is
{
    [Fact]
    public void EnsureGivenTrueCondition()
    {
        Action act = () => Ensure.Argument.Is(true);
        act.Should().NotThrow<ArgumentException>();
    }

    [Fact]
    public void EnsureGivenTrueConditionAndAMessage()
    {
        Action act = () => Ensure.Argument.Is(true, "Test");
        act.Should().NotThrow<ArgumentException>();
    }

    [Fact]
    public void ThrowsArgumentExceptionGivenFalseCondition()
    {
        Action act = () => Ensure.Argument.Is(false);
        act.Should().Throw<ArgumentException>().WithMessage("The expected condition was not exceeded");
    }

    [Fact]
    public void ThrowsArgumentExceptionGivenFalseConditionAndAMessage()
    {
        Action act = () => Ensure.Argument.Is(false, "Test");
        act.Should().Throw<ArgumentException>().WithMessage("Test");
    }
}

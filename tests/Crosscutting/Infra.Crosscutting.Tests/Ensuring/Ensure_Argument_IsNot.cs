using System;
using FluentAssertions;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Ensuring;

public class Ensure_Argument_IsNot
{
    [Fact]
    public void EnsureGivenFalseCondition()
    {
        Action act = () => Ensure.Argument.IsNot(false);
        act.Should().NotThrow<ArgumentException>();
    }

    [Fact]
    public void EnsureGivenFalseConditionAndAMessage()
    {
        Action act = () => Ensure.Argument.IsNot(false, "Test");
        act.Should().NotThrow<ArgumentException>();
    }

    [Fact]
    public void ThrowsArgumentExceptionGivenTrueCondition()
    {
        Action act = () => Ensure.Argument.IsNot(true);
        act.Should().Throw<ArgumentException>().WithMessage("The expected condition was not exceeded");
    }

    [Fact]
    public void ThrowsArgumentExceptionGivenTrueConditionAndAMessage()
    {
        Action act = () => Ensure.Argument.IsNot(true, "Test");
        act.Should().Throw<ArgumentException>().WithMessage("Test");
    }
}

using System;
using FluentAssertions;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Ensuring;

public class Ensure_Not
{
    [Fact]
    public void ThrowExceptionGivenTrue()
    {
        Action act = () => Ensure.Not(true);
        act.Should().Throw<Exception>().And.Message.Should().Be("The expected condition was not exceeded");
    }

    [Fact]
    public void ThrowExceptionGivenTrueAndNotEmptyMessage()
    {
        Action act = () => Ensure.Not(true, "Test");
        act.Should().Throw<Exception>().WithMessage("Test");
    }

    [Fact]
    public void EnsureGivenFalse()
    {
        Action act = () => Ensure.Not(false);
        act.Should().NotThrow<Exception>();
    }

    [Fact]
    public void EnsureInvalidOperationExceptionGivenTrue()
    {
        Action act = () => Ensure.Not<InvalidOperationException>(true);
        act.Should().Throw<InvalidOperationException>().And.Message.Should().Be("The expected condition was not exceeded");
    }

    [Fact]
    public void EnsureInvalidOperationExceptionGivenFalse()
    {
        Action act = () => Ensure.Not<InvalidOperationException>(false);
        act.Should().NotThrow<InvalidOperationException>();
    }

    [Fact]
    public void EnsureGivenFalseAndNotEmptyMessage()
    {
        Action act = () => Ensure.Not(false, "Test");
        act.Should().NotThrow<Exception>();
    }
}

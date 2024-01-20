using System;
using FluentAssertions;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Ensuring;

public class Ensure_That
{
    [Fact]
    public void ThrowExceptionGivenFalseWithoutMessage()
    {
        Action act = () => Ensure.That<InvalidOperationException>(false);
        act.Should().Throw<InvalidOperationException>().And.Message.Should().Be("The expected condition was not exceeded");
    }

    [Fact]
    public void EnsureExceptionGivenTrueWithoutMessage()
    {
        Action act = () => Ensure.That<InvalidOperationException>(true);
        act.Should().NotThrow<InvalidOperationException>();
    }

    [Fact]
    public void ThrowExceptionGivenFalse()
    {
        Action act = () => Ensure.That<InvalidOperationException>(false);
        act.Should().Throw<InvalidOperationException>().And.Message.Should().Be("The expected condition was not exceeded");
    }

    [Fact]
    public void ThrowExceptionGivenFalseAndNotEmptyMessage()
    {
        Action act = () => Ensure.That(false, "Test");
        act.Should().Throw<Exception>().WithMessage("Test");
    }

    [Fact]
    public void EnsureGivenTrue()
    {
        Action act = () => Ensure.That(true);
        act.Should().NotThrow<Exception>();
    }

    [Fact]
    public void EnsureGivenTrueAndNotEmptyMessage()
    {
        Action act = () => Ensure.That(true, "Test");
        act.Should().NotThrow<Exception>();
    }
}

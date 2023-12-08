using System;
using FluentAssertions;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Ensuring;

public class Ensure_Items
{
    [Fact]
    public void ThrowExceptionGivenNullCollection()
    {
        int[] collection = null!;
        Action act = () => Ensure.Items(collection, p => p == 0);
        act.Should().Throw<Exception>().WithMessage("Exception of type 'System.Exception' was thrown.");
    }

    [Fact]
    public void ThrowExceptionNotMatchPredicate()
    {
        int[] collection = new[] { 0 };
        Action act = () => Ensure.Items(collection, p => p > 0);
        act.Should().Throw<Exception>().WithMessage("Exception of type 'System.Exception' was thrown.");
    }

    [Fact]
    public void ThrowExceptionGivenNullCollectionAndMessage()
    {
        int[] collection = null!;
        Action act = () => Ensure.Items(collection, p => p == 0, "Test");
        act.Should().Throw<Exception>().WithMessage("Test");
    }

    [Fact]
    public void ThrowExceptionNotMatchPredicateGivenMessage()
    {
        int[] collection = new[] { 0 };
        Action act = () => Ensure.Items(collection, p => p > 0, "Test");
        act.Should().Throw<Exception>().WithMessage("Test");
    }

    [Fact]
    public void EnsureMatchPredicate()
    {
        int[] collection = new[] { 0 };
        Action act = () => Ensure.Items(collection, p => p == 0);
        act.Should().NotThrow<Exception>();
    }
}

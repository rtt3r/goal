using System;
using FluentAssertions;
using Goal.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Ensuring;

public class Ensure_Equal
{
    [Fact]
    public void ThrowsExceptionGivenNotEqualsPrimitives()
    {
        int a = 1;
        int b = 2;

        Action act = () => Ensure.Equal(a, b);
        act.Should().Throw<Exception>().And.Message.Should().Be(Messages.BothValuesMustBeEqual);
    }

    [Fact]
    public void ThrowsExceptionGivenNotEqualsNonPrimitives()
    {
        var a = new TestObject1();
        var b = new TestObject1();

        Action act = () => Ensure.Equal(a, b);
        act.Should().Throw<Exception>().And.Message.Should().Be(Messages.BothValuesMustBeEqual);
    }

    [Fact]
    public void ThrowsExceptionGivenLeftNull()
    {
        var b = new TestObject1();

        Action act = () => Ensure.Equal(null, b);
        act.Should().Throw<Exception>().And.Message.Should().Be(Messages.BothValuesMustBeEqual);
    }

    [Fact]
    public void ThrowsExceptionGivenRightNull()
    {
        var a = new TestObject1();

        Action act = () => Ensure.Equal(a, null);
        act.Should().Throw<Exception>().And.Message.Should().Be(Messages.BothValuesMustBeEqual);
    }

    [Fact]
    public void EnsureGivenEqualsPrimitives()
    {
        int a = 1;
        int b = 1;

        Action act = () => Ensure.Equal(a, b);
        act.Should().NotThrow<Exception>();
    }

    [Fact]
    public void EnsureGivenEqualsNonPrimitives()
    {
        var a = new TestObject1();
        TestObject1 b = a;

        Action act = () => Ensure.Equal(a, b);
        act.Should().NotThrow<Exception>();
    }
}

using System;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Ensuring;

public class Ensure_Argument_NotNull
{
    [Fact]
    public void EnsureGivenNotNullObject()
    {
        Action act = () => Ensure.Argument.NotNull(new object());
        act.Should().NotThrow<ArgumentNullException>();
    }

    [Fact]
    public void EnsureGivenNotNullObjectAndAParamName()
    {
        var obj = new TestObject1() { TestObject2 = new TestObject2() };

        Action act = () => Ensure.Argument.NotNull(obj, nameof(TestObject1.TestObject2));
        act.Should().NotThrow<ArgumentNullException>();
    }

    [Fact]
    public void EnsureGivenNotNullObjectAndAParamNameAndAMessage()
    {
        var obj = new TestObject1() { TestObject2 = new TestObject2() };

        Action act = () => Ensure.Argument.NotNull(obj.TestObject2, nameof(TestObject1.TestObject2), "Test");
        act.Should().NotThrow<ArgumentNullException>();
    }

    [Fact]
    public void ThrowArgumentNullExceptionGivenNullObject()
    {
        Action act = () => Ensure.Argument.NotNull(null);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().BeNull();
        act.Should().Throw<ArgumentNullException>().And.Message.Should().Be("Object value cannot be null");
    }

    [Fact]
    public void ThrowArgumentNullExceptionGivenNullObjectAndAParamName()
    {
        var obj = new TestObject1();

        Action act = () => Ensure.Argument.NotNull(obj.TestObject2, nameof(TestObject1.TestObject2));
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be(nameof(TestObject1.TestObject2));
        act.Should().Throw<ArgumentNullException>().And.Message.Should().Be("Object value cannot be null (Parameter 'TestObject2')");
    }

    [Fact]
    public void ThrowArgumentNullExceptionGivenNullObjectAndAParamNameAndAMessage()
    {
        var obj = new TestObject1();

        Action act = () => Ensure.Argument.NotNull(obj.TestObject2, nameof(TestObject1.TestObject2), "Test");
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be(nameof(TestObject1.TestObject2));
        act.Should().Throw<ArgumentNullException>().And.Message.Should().Be("Test (Parameter 'TestObject2')");
    }
}

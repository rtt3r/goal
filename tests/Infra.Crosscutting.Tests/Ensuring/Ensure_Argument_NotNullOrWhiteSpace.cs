using System;
using FluentAssertions;
using Goal.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Ensuring
{
    public class Ensure_Argument_NotNullOrWhiteSpace
    {
        [Fact]
        public void EnsureGivenNotNullString()
        {
            Action act = () => Ensure.Argument.NotNullOrWhiteSpace("test");
            act.Should().NotThrow<ArgumentException>();
            act.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public void EnsureGivenNotNullStringAndAParamName()
        {
            var obj = new TestObject1() { Value = "test" };

            Action act = () => Ensure.Argument.NotNullOrWhiteSpace(obj.Value, nameof(TestObject1.Value));
            act.Should().NotThrow<ArgumentException>();
            act.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public void EnsureGivenNotNullStringAndAParamNameAndAMessage()
        {
            var obj = new TestObject1() { Value = "test" };

            Action act = () => Ensure.Argument.NotNullOrWhiteSpace(obj.Value, nameof(TestObject1.Value), "Test");
            act.Should().NotThrow<ArgumentException>();
            act.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public void ThrowArgumentNullExceptionGivenNullString()
        {
            Action act = () => Ensure.Argument.NotNullOrWhiteSpace(null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().BeNull();
            act.Should().Throw<ArgumentNullException>().And.Message.Should().Contain(Messages.StringCannotBeNullOrWhitespace);
        }

        [Fact]
        public void ThrowArgumentNullExceptionGivenNullStringAndAParamName()
        {
            var obj = new TestObject1();

            Action act = () => Ensure.Argument.NotNullOrWhiteSpace(obj.Value, nameof(TestObject1.Value));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be(nameof(TestObject1.Value));
            act.Should().Throw<ArgumentNullException>().And.Message.Should().Contain(Messages.StringCannotBeNullOrWhitespace);
        }

        [Fact]
        public void ThrowArgumentNullExceptionGivenNullStringAndAParamNameAndAMessage()
        {
            var obj = new TestObject1();

            Action act = () => Ensure.Argument.NotNullOrWhiteSpace(obj.Value, nameof(TestObject1.Value), "Test");
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be(nameof(TestObject1.Value));
            act.Should().Throw<ArgumentNullException>().And.Message.Should().Contain("Test");
        }

        [Fact]
        public void ThrowArgumentNullExceptionGivenEmptyString()
        {
            Action act = () => Ensure.Argument.NotNullOrWhiteSpace("");
            act.Should().Throw<ArgumentException>().And.ParamName.Should().BeNullOrEmpty();
            act.Should().Throw<ArgumentException>().And.Message.Should().Contain(Messages.StringCannotBeNullOrWhitespace);
        }

        [Fact]
        public void ThrowArgumentNullExceptionGivenEmptyStringAndAParamName()
        {
            var obj = new TestObject1() { Value = "" };

            Action act = () => Ensure.Argument.NotNullOrWhiteSpace(obj.Value, nameof(TestObject1.Value));
            act.Should().Throw<ArgumentException>().And.ParamName.Should().Be(nameof(TestObject1.Value));
            act.Should().Throw<ArgumentException>().And.Message.Should().Contain(Messages.StringCannotBeNullOrWhitespace);
        }

        [Fact]
        public void ThrowArgumentNullExceptionGivenEmptyStringAndAParamNameAndAMessage()
        {
            var obj = new TestObject1() { Value = "" };

            Action act = () => Ensure.Argument.NotNullOrWhiteSpace(obj.Value, nameof(TestObject1.Value), "Test");
            act.Should().Throw<ArgumentException>().And.ParamName.Should().Be(nameof(TestObject1.Value));
            act.Should().Throw<ArgumentException>().And.Message.Should().Contain("Test");
        }
    }
}

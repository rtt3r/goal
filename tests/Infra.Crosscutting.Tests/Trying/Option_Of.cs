using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_Of
    {
        [Fact]
        public void WithNonNullValue_ReturnsSome()
        {
            int value = 123;
            var result = Option.Of(value);
            result.IsSome.Should().BeTrue();
            result.Value.Should().Be(value);
        }

        [Fact]
        public void WithNullValue_ReturnsNone()
        {
            object value = null;
            var result = Option.Of(value);
            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public void ValueNotNull_ReturnsSome()
        {
            // arrange
            int value = 1;

            // act
            var option = Option.Of(value);

            // assert
            option.IsSome.Should().BeTrue();
            option.Value.Should().Be(value);
        }

        [Fact]
        public void ValueIsNull_ReturnsNone()
        {
            // arrange
            int? value = null;

            // act
            var option = Option.Of(value);

            // assert
            option.IsNone.Should().BeTrue();
        }
    }

    public class Option_Match
    {
        [Fact]
        public void Match_WithSome_SomeActionIsExecuted()
        {
            // arrange
            var option = Helpers.Some(2);
            bool actionPerformed = false;

            // act
            option.Match(
                some: (value) => { actionPerformed = true; },
                none: () => { });

            // assert
            actionPerformed.Should().BeTrue();
        }

        [Fact]
        public void Match_WithSome_ReturnSomeValue()
        {
            // arrange
            var option = Helpers.Some(1);

            // act
            var result = option.Match(some: v => 2, none: () => 0);

            // assert
            result.Should().Be(2);
        }
    }
}
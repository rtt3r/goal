using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_Match
    {
        [Fact]
        public void WithSome_SomeActionIsExecuted()
        {
            // arrange
            var option = Option.Of(2);
            bool actionPerformed = false;

            // act
            option.Match(
                some: (value) => { actionPerformed = true; },
                none: () => { });

            // assert
            actionPerformed.Should().BeTrue();
        }

        [Fact]
        public void WithSome_ReturnSomeValue()
        {
            // arrange
            var option = Option.Of(1);

            // act
            int result = option.Match(some: v => 2, none: () => 0);

            // assert
            result.Should().Be(2);
        }

        [Fact]
        public void WithNone_NoneActionIsExecuted()
        {
            // arrange
            Option<int> option = Option<int>.None;
            bool actionExecuted = false;

            // act
            option.Match(
                some: (value) => { },
                none: () => { actionExecuted = true; });

            // assert
            actionExecuted.Should().BeTrue();
        }

        [Fact]
        public void WithNone_ReturnNoneValue()
        {
            // arrange
            Option<int> option = Option<int>.None;

            // act
            int result = option.Match(some: v => 1, none: () => 0);

            // assert
            result.Should().Be(0);
        }

        [Fact]
        public void WhenSome_ShouldExecuteSomeFunc()
        {
            // Arrange
            var option = Option.Of(42);

            // Act
            string result = option.Match(
                some: value => $"{value} is some",
                none: value => $"{value} is none");

            // Assert
            result.Should().Be("42 is some");
        }

        [Fact]
        public void WhenNone_ShouldExecuteNoneFunc()
        {
            // Arrange
            Option<int> option = Option<int>.None;

            // Act
            string result = option.Match(
                some: value => $"{value} is some",
                none: value => $"{value} is none");

            // Assert
            result.Should().Be("0 is none");  // default(int) is passed to none func since Value property is accessed in None case.
        }
    }
}
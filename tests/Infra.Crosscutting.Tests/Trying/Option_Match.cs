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
            Option<int> option = Helpers.Some(2);
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
            Option<int> option = Helpers.Some(1);

            // act
            int result = option.Match(some: v => 2, none: () => 0);

            // assert
            result.Should().Be(2);
        }

        [Fact]
        public void WithNone_NoneActionIsExecuted()
        {
            // arrange
            Option<int> option = Helpers.None;
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
            Option<int> option = Helpers.None;

            // act
            int result = option.Match(some: v => 1, none: () => 0);

            // assert
            result.Should().Be(0);
        }
    }
}
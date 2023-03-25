using System;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_Map
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(4, 5, 9)]
        public void WithSome_ReturnsSome(int a, int b, int expected)
        {
            var option = Option.Of(a);
            Option<int> result = option.Map(x => x + b);
            result.IsSome.Should().BeTrue();
            result.Value.Should().Be(expected);
        }

        [Fact]
        public void WithNone_ReturnsNone()
        {
            var option = Option.Of<string>(null);
            Option<string> result = option.Map(x => x.ToUpper());
            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public void WithSomeValue_ShouldReturnOptionWithMappedValue()
        {
            // arrange
            int initial = 42;
            var option = Option.Of(initial);

            // act
            Option<string> result = option.Map(i => i.ToString());

            // assert
            result.IsSome.Should().BeTrue();
            result.Value.Should().Be(initial.ToString());
        }

        [Fact]
        public void WithNoneValue_ShouldReturnEmptyOption()
        {
            // arrange
            Option<int> option = Option<int>.None();

            // act
            Option<string> result = option.Map(i => i.ToString());

            // assert
            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public void ReturnsSome_WhenOptionIsSome()
        {
            // Arrange
            var option = Option.Of(42);
            static bool func(int x, string y) => (x + y).Length > 5;

            // Act
            Option<Func<string, bool>> result = option.Map((Func<int, string, bool>)func);

            // Assert
            result.Match(
                some: fn =>
                {
                    fn("hi").Should().BeFalse();
                    fn("hello world").Should().BeTrue();
                },
                none: () => throw new Exception("should have been Some")
            );
        }
    }
}
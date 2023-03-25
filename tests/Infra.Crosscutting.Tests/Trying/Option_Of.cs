using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_Of
    {
        [Fact]
        public void Creates_Option_Of_Some()
        {
            // Arrange & Act
            var option = Option.Of(10);

            // Assert
            option.IsSome.Should().BeTrue();
        }

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
}
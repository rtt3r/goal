using System;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_ImplicitOperator
    {
        [Fact]
        public void FromValue_ReturnsSome()
        {
            // arrange
            int value = 10;

            // act
            Option<int> option = value;

            // assert
            option.IsSome.Should().BeTrue();
            option.Value.Should().Be(value);
        }

        [Fact]
        public void FromNoneType_ReturnsNone()
        {
            // act
            var option = Option<int>.None();

            // assert
            option.IsNone.Should().BeTrue();
        }

        [Fact]
        public void ReturnsNone_WhenOptionIsNone()
        {
            // Arrange
            var option = Option<int>.None();
            static bool func(int x, string y) => (x + y).Length > 5;

            // Act
            Option<Func<string, bool>> result = option.Map((Func<int, string, bool>)func);

            // Assert
            result.Should().Be(Option<Func<string, bool>>.None());
        }
    }
}
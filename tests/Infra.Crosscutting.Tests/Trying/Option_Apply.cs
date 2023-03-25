using System;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_Apply
    {
        [Fact]
        public void Option_Apply_Should_Return_Some_Result_For_Some_Argument()
        {
            // Arrange
            var optionFunc = Option.Of<Func<int, bool>>(x => x > 5);
            var optionArg = Option.Of(6);

            // Act
            var result = optionFunc.Apply(optionArg);

            // Assert
            result.IsSome.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Fact]
        public void Option_Apply_Should_Return_None_Result_For_None_Function()
        {
            // Arrange
            Option<Func<int, bool>> optionFunc = Helpers.None;
            var optionArg = Option.Of(6);

            // Act
            var result = optionFunc.Apply(optionArg);

            // Assert
            result.IsNone.Should().BeTrue();
        }
    }
}
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
            Option<bool> result = optionFunc.Apply(optionArg);

            // Assert
            result.IsSome.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Fact]
        public void BothOptionsHaveValues_ReturnsNewOptionWithResult()
        {
            // Arrange
            Option<Func<int, bool, string>> inputFuncOption = Helpers.Some<Func<int, bool, string>>((a, b) => $"{a} {b}");
            Option<int> inputValueOption = Helpers.Some(42);

            // Act
            Option<Func<bool, string>> resultOption = inputFuncOption.Apply(inputValueOption);
            string funcResult = resultOption.Value.Invoke(false);
            string someResult = Helpers.Some("42 False").Value;

            // Assert
            funcResult.Should().BeEquivalentTo(someResult);
        }

        [Fact]
        public void FirstOptionIsEmpty_ReturnsEmptyOption()
        {
            // Arrange
            Option<Func<int, string, double>> inputFuncOption = Helpers.None;
            Option<int> inputValueOption = Helpers.Some(42);

            // Act
            Option<Func<string, double>> resultOption = inputFuncOption.Apply(inputValueOption);

            // Assert
            resultOption.Value.Should().BeNull();
        }

        [Fact]
        public void Option_Apply_Should_Return_None_Result_For_None_Function()
        {
            // Arrange
            Option<Func<int, bool>> optionFunc = Helpers.None;
            var optionArg = Option.Of(6);

            // Act
            Option<bool> result = optionFunc.Apply(optionArg);

            // Assert
            result.IsNone.Should().BeTrue();
        }
    }
}
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Option_OrElse
    {
        [Fact]
        public void GivenAnOption_WhenOrElseWithOptionIsCalled_AndTheOptionHasValue_ShouldReturnOriginalOption()
        {
            //Arrange
            var option1 = Option.Of(10);
            var option2 = Option.Of(20);

            //Act
            Option<int> result = option1.OrElse(option2);

            //Assert
            result.Should().Be(option1);
        }

        [Fact]
        public void GivenAnOption_WhenOrElseWithOptionIsCalled_AndTheOptionHasNoValue_ShouldReturnBackupOption()
        {
            //Arrange
            Option<int> option1 = Option<int>.None;
            var option2 = Option.Of(20);

            //Act
            Option<int> result = option1.OrElse(option2);

            //Assert
            result.Should().Be(option2);
        }

        [Fact]
        public void GivenAnOption_WhenOrElseWithFuncIsCalled_AndTheOptionHasValue_ShouldReturnOriginalOption()
        {
            //Arrange
            var option1 = Option.Of("Hello World");

            //Act
            Option<string> result = option1.OrElse(() => Option.Of("Fallback"));

            //Assert
            result.Should().Be(option1);
        }

        [Fact]
        public void GivenAnOption_WhenOrElseWithFuncIsCalled_AndTheOptionHasNoValue_ShouldReturnFromTheFallbackFunction()
        {
            //Arrange
            Option<string> option1 = Option<string>.None;

            //Act
            Option<string> result = option1.OrElse(() => Option.Of("Fallback"));

            //Assert
            var expectedOption = Option.Of("Fallback");
            result.Should().Be(expectedOption);
        }
    }

}
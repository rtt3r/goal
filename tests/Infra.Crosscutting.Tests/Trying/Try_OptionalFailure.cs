using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Try_OptionalFailure
    {
        [Fact]
        public void IsNullForSuccessTry()
        {
            int value = 456;
            Try<string, int> success = value;

            Option<string> optionalFailure = success.OptionalFailure;

            optionalFailure.Should().Be(Option.Of<string>(null));
        }

        [Fact]
        public void IsSomeForFailureTry()
        {
            string errorMessage = "Failed!";
            Try<string, int> failure = errorMessage;

            Option<string> optionalFailure = failure.OptionalFailure;

            optionalFailure.Should().Be(Option.Of(errorMessage));
        }
    }
}
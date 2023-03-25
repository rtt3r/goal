using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Try_OptionalSuccess
    {
        [Fact]
        public void IsNoneForFailureTry()
        {
            string errorMessage = "Failed!";
            Try<string, int> failure = errorMessage;

            Option<int> optionalSuccess = failure.OptionalSuccess;

            optionalSuccess.Should().Be(Option<int>.None());
        }

        [Fact]
        public void IsSomeForSuccessTry()
        {
            int value = 123;
            Try<string, int> success = value;

            Option<int> optionalSuccess = success.OptionalSuccess;

            optionalSuccess.Should().Be(Option.Of(value));
        }
    }
}
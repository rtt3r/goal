using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying
{
    public class Try_ImplicitOperation
    {
        [Fact]
        public void ReturnsSuccessTry()
        {
            int value = 42;

            Try<string, int> result = value;

            result.IsSuccess.Should().BeTrue();
            result.GetSuccess().Should().Be(value);
        }

        [Fact]
        public void ReturnsFailureTry()
        {
            string error = "oops!";

            Try<string, int> result = error;

            result.IsFailure.Should().BeTrue();
            result.GetFailure().Should().Be(error);
        }
    }
}
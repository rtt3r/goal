using FluentAssertions;
using Goal.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Trying;

public class Try_OptionalSuccess
{
    [Fact]
    public void IsNoneForFailureTry()
    {
        string errorMessage = "Failed!";
        Try<int, string> failure = errorMessage;

        Option<int> optionalSuccess = failure.OptionalSuccess;

        optionalSuccess.Should().Be(Option<int>.None());
    }

    [Fact]
    public void IsSomeForSuccessTry()
    {
        int value = 123;
        Try<int, string> success = value;

        Option<int> optionalSuccess = success.OptionalSuccess;

        optionalSuccess.Should().Be(Option.Of(value));
    }
}
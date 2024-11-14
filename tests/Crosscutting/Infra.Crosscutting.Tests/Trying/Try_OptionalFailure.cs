using FluentAssertions;
using Goal.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Trying;

public class Try_OptionalFailure
{
    [Fact]
    public void IsNullForSuccessTry()
    {
        int value = 456;
        Try<int, string> success = value;

        Option<string?> optionalFailure = success.OptionalFailure;

        optionalFailure.Should().Be(Option.Of<string>(null!));
    }

    [Fact]
    public void IsSomeForFailureTry()
    {
        string errorMessage = "Failed!";
        Try<int, string> failure = errorMessage;

        Option<string?> optionalFailure = failure.OptionalFailure;

        optionalFailure.Should().Be(Option.Of(errorMessage));
    }
}
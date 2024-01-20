using FluentAssertions;
using Goal.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Trying;

public class Try_Failure
{
    [Fact]
    public void MatchOnFailure()
    {
        string errorMessage = "Something went wrong";
        Try<string, int> failure = errorMessage;

        bool didCallFailure = false;
        bool didCallSuccess = false;

        failure.Match(
            err =>
            {
                didCallFailure = true;
                err.Should().Be(errorMessage);
            },
            success => didCallSuccess = true
        );

        didCallFailure.Should().BeTrue();
        didCallSuccess.Should().BeFalse();
    }
}
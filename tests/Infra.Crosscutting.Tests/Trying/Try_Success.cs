using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Trying;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Trying;

public class Try_Success
{
    [Fact]
    public void MatchOnSuccess()
    {
        int value = 10;
        Try<string, int> success = value;

        bool didCallFailure = false;
        bool didCallSuccess = false;

        success.Match(
            err => didCallFailure = true,
            i =>
            {
                didCallSuccess = true;
                i.Should().Be(value);
            }
        );

        didCallFailure.Should().BeFalse();
        didCallSuccess.Should().BeTrue();
    }
}
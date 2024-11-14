using FluentAssertions;
using Goal.Infra.Crosscutting.Errors;
using Goal.Infra.Crosscutting.Extensions;
using OneOf;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Extensions;

public class OneOf_GetSuccess
{
    [Fact]
    public void ReturnTrueGivenSuccess()
    {
        OneOf<bool, AppError> oneOf = true;

        oneOf.GetSuccess().Should().BeTrue();
    }

    [Fact]
    public void ReturnFalseGivenSuccess()
    {
        OneOf<bool, AppError> oneOf = false;

        oneOf.GetSuccess().Should().BeFalse();
    }
}

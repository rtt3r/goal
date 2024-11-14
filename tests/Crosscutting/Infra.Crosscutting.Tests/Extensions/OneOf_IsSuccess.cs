using FluentAssertions;
using Goal.Infra.Crosscutting.Errors;
using Goal.Infra.Crosscutting.Extensions;
using OneOf;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Extensions;

public class OneOf_IsSuccess
{
    [Fact]
    public void ReturnTrueGivenSuccess()
    {
        OneOf<bool, AppError> oneOf = true;

        oneOf.IsSuccess().Should().BeTrue();
    }

    [Fact]
    public void ReturnFalseGivenError()
    {
        OneOf<bool, AppError> oneOf = new AppError(ErrorType.UnexpectedError, "Something went wrong", nameof(ErrorType.UnexpectedError));

        oneOf.IsSuccess().Should().BeFalse();
    }
}

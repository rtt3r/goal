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
    public void ReturnFalseGivenAppError()
    {
        OneOf<bool, AppError> oneOf = new AppError(ErrorType.UnexpectedError, "Something went wrong", nameof(ErrorType.UnexpectedError));

        oneOf.IsSuccess().Should().BeFalse();
    }

    [Fact]
    public void ReturnFalseGivenTestAppError()
    {
        OneOf<bool, TestAppError> oneOf = new TestAppError("Something went wrong");

        oneOf.IsSuccess().Should().BeFalse();
    }

    private record TestAppError(string Detail)
        : AppError(ErrorType.UnexpectedError, Detail, "TEST_ERROR");
}

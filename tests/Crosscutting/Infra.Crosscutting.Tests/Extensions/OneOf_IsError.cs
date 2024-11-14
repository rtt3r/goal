using FluentAssertions;
using Goal.Infra.Crosscutting.Errors;
using Goal.Infra.Crosscutting.Extensions;
using OneOf;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Extensions;

public class OneOf_IsError
{
    [Fact]
    public void ReturnTrueGivenAppError()
    {
        OneOf<bool, AppError> oneOf = new AppError(ErrorType.UnexpectedError, "Something went wrong", nameof(ErrorType.UnexpectedError));

        oneOf.IsError().Should().BeTrue();
    }

    [Fact]
    public void ReturnTrueGivenTestAppError()
    {
        OneOf<bool, TestAppError> oneOf = new TestAppError("Something went wrong");

        oneOf.IsError().Should().BeTrue();
    }

    [Fact]
    public void ReturnFalseGivenSuccess()
    {
        OneOf<bool, AppError> oneOf = true;

        oneOf.IsError().Should().BeFalse();
    }

    private record TestAppError(string Detail)
        : AppError(ErrorType.UnexpectedError, Detail, "TEST_ERROR");
}

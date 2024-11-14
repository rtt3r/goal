using FluentAssertions;
using Goal.Infra.Crosscutting.Errors;
using Goal.Infra.Crosscutting.Extensions;
using OneOf;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Extensions;

public class OneOf_GetError
{
    [Fact]
    public void ReturnErrorGivenAppError()
    {
        OneOf<bool, AppError> oneOf = new AppError(ErrorType.UnexpectedError, "Something went wrong", nameof(ErrorType.UnexpectedError));

        oneOf.GetError().Type.Should().Be(ErrorType.UnexpectedError);
        oneOf.GetError().Detail.Should().Be("Something went wrong");
        oneOf.GetError().Code.Should().Be(nameof(ErrorType.UnexpectedError));
    }

    [Fact]
    public void ReturnFalseGivenSuccess()
    {
        OneOf<bool, TestAppError> oneOf = new TestAppError("This is a test error");

        oneOf.GetError().Type.Should().Be(ErrorType.UnexpectedError);
        oneOf.GetError().Detail.Should().Be("This is a test error");
        oneOf.GetError().Code.Should().Be("TEST_ERROR");
    }

    private record TestAppError(string Detail)
        : AppError(ErrorType.UnexpectedError, Detail, "TEST_ERROR");
}
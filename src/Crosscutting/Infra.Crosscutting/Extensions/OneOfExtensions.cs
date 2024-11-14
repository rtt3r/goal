using Goal.Infra.Crosscutting.Errors;
using OneOf;

namespace Goal.Infra.Crosscutting.Extensions;

public static class OneOfExtensions
{
    public static bool IsSuccess<TResult>(this OneOf<TResult, AppError> obj)
       => obj.IsT0;

    public static bool IsError<TResult>(this OneOf<TResult, AppError> obj)
        => obj.IsT1;

    public static TResult GetSuccess<TResult>(this OneOf<TResult, AppError> obj)
       => obj.AsT0;

    public static AppError GetError<TResult>(this OneOf<TResult, AppError> obj)
        => obj.AsT1;

    public static TError GetError<TResult, TError>(this OneOf<TResult, TError> obj)
        where TError : AppError
        => obj.AsT1;
}
using System;

namespace Goal.Infra.Crosscutting.Trying;

public readonly struct Try<TSuccess, TFailure>
{
    internal TFailure? Failure { get; }

    internal TSuccess? Success { get; }

    public bool IsFailure { get; }

    public bool IsSuccess => !IsFailure;

    public Option<TFailure?> OptionalFailure => !IsFailure
        ? (Option<TFailure?>)Option.None()
        : Option.Of(Failure);

    public Option<TSuccess?> OptionalSuccess => !IsSuccess
        ? (Option<TSuccess?>)Option.None()
        : Option.Of(Success);

    internal Try(TFailure failure)
    {
        IsFailure = true;
        Failure = failure;
        Success = default;
    }

    internal Try(TSuccess success)
    {
        IsFailure = false;
        Failure = default;
        Success = success;
    }

    public TResult Match<TResult>(Func<TSuccess?, TResult> success, Func<TFailure?, TResult> failure)
        => !IsFailure ? success(Success) : failure(Failure);

    public UnitType Match(Action<TSuccess?> success, Action<TFailure?> failure)
        => Match(Helpers.ToFunc(success), Helpers.ToFunc(failure));

    public TSuccess? GetSuccess()
        => Success;

    public TFailure? GetFailure()
        => Failure;

    public static implicit operator Try<TSuccess, TFailure>(TFailure failure)
        => new(failure);

    public static implicit operator Try<TSuccess, TFailure>(TSuccess success)
        => new(success);

    public static Try<TSuccess, TFailure> Of(TSuccess obj)
        => obj;

    public static Try<TSuccess, TFailure> Of(TFailure obj)
        => obj;
}

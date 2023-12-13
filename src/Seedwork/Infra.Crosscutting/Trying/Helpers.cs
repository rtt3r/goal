using System;

namespace Goal.Seedwork.Infra.Crosscutting.Trying;

public static class Helpers
{
    public static Func<TA, Func<TB, TResult>> Curry<TA, TB, TResult>(this Func<TA, TB, TResult> func)
        => (TA a) => (TB b) => func(a, b);

    public static Func<T, UnitType> ToFunc<T>(Action<T> action)
    {
        return (T o) =>
        {
            action(o);
            return Option.Unit();
        };
    }

    public static Func<UnitType> ToFunc(Action action)
    {
        return delegate
        {
            action();
            return Option.Unit();
        };
    }
}

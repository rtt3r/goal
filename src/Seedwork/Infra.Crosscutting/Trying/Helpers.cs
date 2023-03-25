using System;

namespace Goal.Seedwork.Infra.Crosscutting.Trying
{
    public static class Helpers
    {
        public static readonly NoneType None;

        private static readonly Unit unit = default;

        public static Func<TA, Func<TB, TResult>> Curry<TA, TB, TResult>(this Func<TA, TB, TResult> func)
            => (TA a) => (TB b) => func(a, b);

        public static Func<TA, Func<TB, Func<TC, TResult>>> Curry<TA, TB, TC, TResult>(this Func<TA, TB, TC, TResult> func)
            => (TA a) => (TB b) => (TC c) => func(a, b, c);

        public static Unit Unit()
            => unit;

        public static Func<T, Unit> ToFunc<T>(Action<T> action)
        {
            return (T o) =>
            {
                action(o);
                return Unit();
            };
        }

        public static Func<Unit> ToFunc(Action action)
        {
            return delegate
            {
                action();
                return Unit();
            };
        }
    }
}

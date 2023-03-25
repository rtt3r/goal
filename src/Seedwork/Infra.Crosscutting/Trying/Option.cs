using System;

namespace Goal.Seedwork.Infra.Crosscutting.Trying
{
    public static class Option
    {
        private static readonly NoneType none = default;
        private static readonly UnitType unit = default;

        public static Option<T> Of<T>(T value)
            => new(value, value != null);

        public static NoneType None()
            => none;

        public static UnitType Unit()
            => unit;

        public static Option<TResult> Apply<T, TResult>(this Option<Func<T, TResult>> @this, Option<T> arg)
            => @this.Bind((Func<T, TResult> f) => arg.Map(f));

        public static Option<Func<TB, TResult>> Apply<TA, TB, TResult>(this Option<Func<TA, TB, TResult>> @this, Option<TA> arg)
            => @this.Map(Helpers.Curry).Apply(arg);

        public static Option<TR> Map<T, TR>(this Option<T> @this, Func<T, TR> mapfunc)
        {
            if (!@this.IsSome)
            {
                return Option.None();
            }

            return Option.Of(mapfunc(@this.Value));
        }

        public static Option<Func<TB, TResult>> Map<TA, TB, TResult>(this Option<TA> @this, Func<TA, TB, TResult> func)
            => @this.Map(func.Curry());

        public static Option<TR> Bind<T, TR>(this Option<T> @this, Func<T, Option<TR>> bindfunc)
        {
            if (!@this.IsSome)
            {
                return Option.None();
            }

            return bindfunc(@this.Value);
        }

        public static T GetOrElse<T>(this Option<T> @this, Func<T> fallback)
            => @this.Match((T value) => value, fallback);

        public static T GetOrElse<T>(this Option<T> @this, T @else)
            => @this.GetOrElse(() => @else);

        public static Option<T> OrElse<T>(this Option<T> @this, Option<T> @else)
            => @this.Match((T _) => @this, () => @else);

        public static Option<T> OrElse<T>(this Option<T> @this, Func<Option<T>> fallback)
            => @this.Match((T _) => @this, fallback);

        public static Option<T> IfNone<T>(this Option<T> @this, Action action)
        {
            if (@this.IsNone)
            {
                action();
            }

            return @this;
        }
    }
}

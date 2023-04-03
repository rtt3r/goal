using System;

namespace Goal.Seedwork.Infra.Crosscutting.Trying
{
    public readonly struct Option<T>
    {
        private static readonly Option<T> none = default;

        public T Value { get; }

        public bool IsSome { get; }

        public bool IsNone => !IsSome;

        public static Option<T> None()
            => none;

        internal Option(T value, bool isSome)
        {
            Value = value;
            IsSome = isSome;
        }

        public TR Match<TR>(Func<T, TR> some, Func<TR> none) => !IsSome ? none() : some(Value);

        public UnitType Match(Action<T> some, Action none)
            => Match(Helpers.ToFunc(some), Helpers.ToFunc(none));

        public TR Match<TR>(Func<T, TR> some, Func<T, TR> none) => !IsSome ? none(Value) : some(Value);

        public static implicit operator Option<T>(T value)
            => Option.Of(value);

        public static implicit operator Option<T>(NoneType _)
            => Option<T>.None();

        public static implicit operator UnitType(Option<T> _)
            => Option.Unit();
    }
}

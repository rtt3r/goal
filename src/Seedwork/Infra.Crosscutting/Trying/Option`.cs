using System;

namespace Goal.Seedwork.Infra.Crosscutting.Trying
{
    public struct Option<T>
    {
        public static readonly Option<T> None;

        public T Value { get; }

        public bool IsSome { get; }

        public bool IsNone => !IsSome;

        internal Option(T value, bool isSome)
        {
            Value = value;
            IsSome = isSome;
        }

        public TR Match<TR>(Func<T, TR> some, Func<TR> none)
        {
            if (!IsSome)
            {
                return none();
            }

            return some(Value);
        }

        public Unit Match(Action<T> some, Action none)
            => Match(Helpers.ToFunc(some), Helpers.ToFunc(none));

        public TR Match<TR>(Func<T, TR> some, Func<T, TR> none)
        {
            if (!IsSome)
            {
                return none(Value);
            }

            return some(Value);
        }

        public static implicit operator Option<T>(T value)
            => Helpers.Some(value);

        public static implicit operator Option<T>(NoneType _)
            => None;

        public static implicit operator Unit(Option<T> _)
            => Helpers.Unit();
    }
}

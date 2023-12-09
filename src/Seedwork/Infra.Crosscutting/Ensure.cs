using System;
using System.Collections.Generic;
using System.Linq;
using Goal.Seedwork.Infra.Crosscutting.Extensions;

namespace Goal.Seedwork.Infra.Crosscutting;

// [DebuggerStepThrough]
public static class Ensure
{
    public static void That(bool condition, string? message = null)
        => That<Exception>(condition, message);

    public static void That(Func<bool> predicate, string? message = null)
        => That(predicate(), message);

    public static void That<TException>(bool condition, string? message = null)
        where TException : Exception
    {
        if (!condition)
        {
            var exception = (TException?)Activator.CreateInstance(typeof(TException), message);

            if (exception is not null)
            {
                throw exception;
            }

            throw new InvalidOperationException("Exception was note created");
        }
    }

    public static void That<TException>(Func<bool> predicate, string? message = null)
        where TException : Exception => That<TException>(predicate(), message);

    public static void Not<TException>(bool condition, string? message = null)
        where TException : Exception => That<TException>(!condition, message);

    public static void Not(bool condition, string? message = null)
        => Not<Exception>(condition, message);

    public static void NotNull(object? value, string? message = null)
        => That<NullReferenceException>(value is not null, message);

    public static void NotNullOrEmpty(string? value)
        => NotNullOrEmpty(value, null);

    public static void NotNullOrEmpty(string? value, string? message = null)
        => That(!string.IsNullOrEmpty(value), message ?? Messages.StringCannotBeNullOrEmpty);

    public static void NotNullOrWhiteSpace(string? value)
        => NotNullOrWhiteSpace(value, null);

    public static void NotNullOrWhiteSpace(string? value, string? message = null)
        => That(!string.IsNullOrWhiteSpace(value), message ?? Messages.StringCannotBeNullOrWhitespace);

    public static void Equal<T>(T left, T right)
        => Equal(left, right, null);

    public static void Equal<T>(T left, T right, string? message = null)
        => That(left != null && right != null && left.Equals(right), message ?? Messages.BothValuesMustBeEqual);

    public static void NotEqual<T>(T left, T right)
        => NotEqual(left, right, null);

    public static void NotEqual<T>(T left, T right, string? message = null)
        => That(left != null && right != null && !left.Equals(right), message ?? Messages.BothValuesMustNotBeEqual);

    public static void Items<T>(IEnumerable<T> collection, Func<T, bool> predicate)
        => Items(collection, predicate, null);

    public static void Items<T>(IEnumerable<T> collection, Func<T, bool> predicate, string? message = null)
        => That(collection is not null && !collection.Any(x => !predicate(x)), message);

    public struct Argument
    {
        public static void Is(bool condition)
            => Is(condition, null);

        public static void Is(bool condition, string? message = null)
            => That<ArgumentException>(condition, message);

        public static void IsNot(bool condition)
            => IsNot(condition, null);

        public static void IsNot(bool condition, string? message = null)
            => Is(!condition, message);

        public static void IsNotNull(object? value)
            => IsNotNull(value, null, null);

        public static void IsNotNull(object? value, string? paramName)
            => IsNotNull(value, paramName, null);

        public static void IsNotNull(object? value, string? paramName, string? message = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName, message ?? Messages.ObjectValueCannotBeNull);
            }
        }

        public static void IsNotNullOrEmpty(string? value)
            => IsNotNullOrEmpty(value, null, null);

        public static void IsNotNullOrEmpty(string? value, string? paramName)
            => IsNotNullOrEmpty(value, paramName, null);

        public static void IsNotNullOrEmpty(string? value, string? paramName, string? message = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName, message ?? Messages.StringCannotBeNullOrEmpty);
            }

            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(message ?? Messages.StringCannotBeNullOrEmpty, paramName);
            }
        }

        public static void IsNotNullOrWhiteSpace(string? value)
            => IsNotNullOrWhiteSpace(value, null, null);

        public static void IsNotNullOrWhiteSpace(string? value, string? paramName)
            => IsNotNullOrWhiteSpace(value, paramName, null);

        public static void IsNotNullOrWhiteSpace(string? value, string? paramName, string? message = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName, message ?? Messages.StringCannotBeNullOrWhitespace);
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message ?? Messages.StringCannotBeNullOrWhitespace, paramName);
            }
        }
    }
}

using System;
using System.Diagnostics;
using Goal.Seedwork.Infra.Crosscutting.Extensions;

namespace Goal.Seedwork.Infra.Crosscutting;

[DebuggerStepThrough]
public static class Ensure
{
    public static void That(bool condition)
        => That(condition, Messages.ExpectedConditionWasNotExceeded);

    public static void That(bool condition, string message)
        => That<Exception>(condition, message);

    public static void That(Func<bool> predicate)
        => That(predicate(), Messages.ExpectedConditionWasNotExceeded);

    public static void That(Func<bool> predicate, string message)
        => That(predicate(), message);

    public static void That<TException>(bool condition)
        where TException : Exception
        => That<TException>(condition, Messages.ExpectedConditionWasNotExceeded);

    public static void That<TException>(bool condition, string message)
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

    public static void That<TException>(Func<bool> predicate)
        where TException : Exception
        => That<TException>(predicate(), Messages.ExpectedConditionWasNotExceeded);

    public static void That<TException>(Func<bool> predicate, string message)
        where TException : Exception
        => That<TException>(predicate(), message);

    public static void Not(bool condition)
        => Not(condition, Messages.ExpectedConditionWasNotExceeded);

    public static void Not(bool condition, string message)
        => Not<Exception>(condition, message);

    public static void Not(Func<bool> predicate)
        => Not(predicate(), Messages.ExpectedConditionWasNotExceeded);

    public static void Not(Func<bool> predicate, string message)
        => Not<Exception>(predicate(), message);

    public static void Not<TException>(bool condition)
        where TException : Exception
        => Not<TException>(condition, Messages.ExpectedConditionWasNotExceeded);

    public static void Not<TException>(bool condition, string message)
        where TException : Exception
        => That<TException>(!condition, message);

    public static void Not<TException>(Func<bool> predicate)
        where TException : Exception
        => Not<TException>(predicate, Messages.ExpectedConditionWasNotExceeded);

    public static void Not<TException>(Func<bool> predicate, string message)
        where TException : Exception
        => Not<TException>(predicate(), message);

    public static void NotNull(object? value)
        => NotNull(value, "The value provided can't be null");

    public static void NotNull(object? value, string message)
        => That<NullReferenceException>(value is not null, message);

    public static void NotNullOrEmpty(string? value)
        => NotNullOrEmpty(value, Messages.ValueProvidedCannotBeNullOrEmpty);

    public static void NotNullOrEmpty(string? value, string message)
        => That(!string.IsNullOrEmpty(value), message);

    public static void NotNullOrWhiteSpace(string? value)
        => NotNullOrWhiteSpace(value, Messages.ValueProvidedCannotBeNullOrWhitespace);

    public static void NotNullOrWhiteSpace(string? value, string message)
        => That(!string.IsNullOrWhiteSpace(value), message);

    public static void Equal<T>(T left, T right)
        => Equal(left, right, Messages.BothValuesMustBeEqual);

    public static void Equal<T>(T left, T right, string message)
        => That(left != null && right != null && left.Equals(right), message);

    public static void NotEqual<T>(T left, T right)
        => NotEqual(left, right, Messages.BothValuesMustNotBeEqual);

    public static void NotEqual<T>(T left, T right, string message)
        => That(left != null && right != null && !left.Equals(right), message);

    public struct Argument
    {
        public static void Is(bool condition)
            => Is(condition, Messages.ExpectedConditionWasNotExceeded);

        public static void Is(bool condition, string message)
            => That<ArgumentException>(condition, message);

        public static void IsNot(bool condition)
            => IsNot(condition, Messages.ExpectedConditionWasNotExceeded);

        public static void IsNot(bool condition, string message)
            => Is(!condition, message);

        public static void IsNotNull(object? value)
            => IsNotNull(value, null, Messages.ValueProvidedCannotBeNull);

        public static void IsNotNull(object? value, string? paramName)
            => IsNotNull(value, paramName, Messages.ValueProvidedCannotBeNull);

        public static void IsNotNull(object? value, string? paramName, string message)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        public static void IsNotNullOrEmpty(string? value)
            => IsNotNullOrEmpty(value, null, Messages.ValueProvidedCannotBeNullOrEmpty);

        public static void IsNotNullOrEmpty(string? value, string? paramName)
            => IsNotNullOrEmpty(value, paramName, Messages.ValueProvidedCannotBeNullOrEmpty);

        public static void IsNotNullOrEmpty(string? value, string? paramName, string message)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName, message);
            }

            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(message ?? Messages.ValueProvidedCannotBeNullOrEmpty, paramName);
            }
        }

        public static void IsNotNullOrWhiteSpace(string? value)
            => IsNotNullOrWhiteSpace(value, null, Messages.ValueProvidedCannotBeNullOrWhitespace);

        public static void IsNotNullOrWhiteSpace(string? value, string? paramName)
            => IsNotNullOrWhiteSpace(value, paramName, Messages.ValueProvidedCannotBeNullOrWhitespace);

        public static void IsNotNullOrWhiteSpace(string? value, string? paramName, string message)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName, message);
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message, paramName);
            }
        }
    }
}

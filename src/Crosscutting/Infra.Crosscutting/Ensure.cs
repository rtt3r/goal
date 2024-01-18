using System;
using System.Diagnostics;

namespace Goal.Seedwork.Infra.Crosscutting;

[DebuggerStepThrough]
public static class Ensure
{
    public static void That(bool condition)
        => That(condition, Messages.ExpectedConditionWasNotExceeded);

    public static void That(bool condition, string message)
        => That<InvalidOperationException>(condition, message);

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

    public static void Not(bool condition)
        => Not(condition, Messages.ExpectedConditionWasNotExceeded);

    public static void Not(bool condition, string message)
        => Not<InvalidOperationException>(condition, message);

    public static void Not<TException>(bool condition)
        where TException : Exception
        => Not<TException>(condition, Messages.ExpectedConditionWasNotExceeded);

    public static void Not<TException>(bool condition, string message)
        where TException : Exception
        => That<TException>(!condition, message);

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
    }
}

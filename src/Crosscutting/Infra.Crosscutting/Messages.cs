using System.Diagnostics;

namespace Goal.Seedwork.Infra.Crosscutting;

[DebuggerStepThrough]
public static class Messages
{
    public const string ValueProvidedCannotBeNullOrWhitespace = "String value cannot be null or whitespace";
    public const string ValueProvidedCannotBeNullOrEmpty = "The value provided can't be null or empty";
    public const string ValueProvidedCannotBeNull = "Object value cannot be null";
    public const string BothValuesMustNotBeEqual = "Both values must not be equal";
    public const string BothValuesMustBeEqual = "Both values must be equal";
    public const string ExpectedConditionWasNotExceeded = "The expected condition was not exceeded";
}

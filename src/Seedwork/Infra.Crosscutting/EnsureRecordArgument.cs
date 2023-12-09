namespace Goal.Seedwork.Infra.Crosscutting;

public class EnsureRecordArgument
{
    public static T EnsureNotNull<T>(T? value)
    {
        Ensure.Argument.IsNotNull(value);
        return value!;
    }

    public static T EnsureNotNull<T>(T? value, string? paramName)
    {
        Ensure.Argument.IsNotNull(value, paramName);
        return value!;
    }

    public static T EnsureNotNull<T>(T? value, string? paramName, string? message = null)
    {
        Ensure.Argument.IsNotNull(value, paramName, message);
        return value!;
    }

    public static string EnsureNotNullOrEmpty(string? value)
    {
        Ensure.Argument.IsNotNullOrEmpty(value);
        return value!;
    }

    public static string EnsureNotNullOrEmpty(string? value, string? paramName)
    {
        Ensure.Argument.IsNotNullOrEmpty(value, paramName);
        return value!;
    }

    public static string EnsureNotNullOrEmpty(string? value, string? paramName, string? message = null)
    {
        Ensure.Argument.IsNotNullOrEmpty(value, paramName, message);
        return value!;
    }

    public static string EnsureNotNullOrWhiteSpace(string? value)
    {
        Ensure.Argument.IsNotNullOrWhiteSpace(value);
        return value!;
    }

    public static string EnsureNotNullOrWhiteSpace(string? value, string? paramName)
    {
        Ensure.Argument.IsNotNullOrWhiteSpace(value, paramName);
        return value!;
    }

    public static string EnsureNotNullOrWhiteSpace(string? value, string? paramName, string? message = null)
    {
        Ensure.Argument.IsNotNullOrWhiteSpace(value, paramName, message);
        return value!;
    }
}
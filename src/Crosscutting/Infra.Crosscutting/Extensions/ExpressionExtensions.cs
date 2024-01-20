using System;
using System.Linq.Expressions;

namespace Goal.Infra.Crosscutting.Extensions;

public static partial class ExtensionManager
{
    public static string GetPropertyName<TSource, TProp>(this Expression<Func<TSource, TProp>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        if (predicate.Body is MemberExpression bodyMemberExpression)
            return bodyMemberExpression.Member.Name;

        return predicate.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operandMemberExpression
            ? operandMemberExpression.Member.Name
            : throw new ArgumentException($"Expression not supported.", nameof(predicate));
    }
}

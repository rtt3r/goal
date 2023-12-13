using System;
using System.Linq.Expressions;

namespace Goal.Seedwork.Infra.Crosscutting.Extensions;

public static partial class ExtensionManager
{
    public static string GetPropertyName<TSource, TProp>(this Expression<Func<TSource, TProp>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        if (predicate.Body is MemberExpression bodyMemberExpression)
            return bodyMemberExpression.Member.Name;

        if (predicate.Body is UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MemberExpression operandMemberExpression)
                return operandMemberExpression.Member.Name;
        }

        throw new ArgumentException($"Expression not supported.", nameof(predicate));
    }
}

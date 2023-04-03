using System;
using System.Linq.Expressions;

namespace Goal.Seedwork.Infra.Crosscutting.Extensions;

public static partial class ExtensionManager
{
    public static string GetPropertyName<TSource, TProp>(this Expression<Func<TSource, TProp>> predicate)
    {
        Ensure.Argument.IsNotNull(predicate, nameof(predicate));

        return predicate.Body is MemberExpression memberExpression
            ? memberExpression.Member.Name
            : predicate.Body is UnaryExpression unaryExpression
            ? (unaryExpression.Operand as MemberExpression).Member.Name
            : throw new ArgumentException($"Expression not supported.", nameof(predicate));
    }
}

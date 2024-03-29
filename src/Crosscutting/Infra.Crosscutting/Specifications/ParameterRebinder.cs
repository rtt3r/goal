using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Goal.Infra.Crosscutting.Specifications;

public sealed class ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map) : ExpressionVisitor
{
    private readonly Dictionary<ParameterExpression, ParameterExpression> map = map ?? [];

    public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        => new ParameterRebinder(map).Visit(exp);

    protected override Expression VisitParameter(ParameterExpression node)
    {
        ParameterExpression? replacement = map.GetValueOrDefault(node)
            ?? throw new ArgumentNullException(nameof(node));

        node = replacement;

        return base.VisitParameter(node);
    }
}

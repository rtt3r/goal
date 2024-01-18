using System;
using System.Linq.Expressions;

namespace Goal.Seedwork.Infra.Crosscutting.Specifications;

public sealed class TrueSpecification<TEntity> : Specification<TEntity>
    where TEntity : class
{
    public override Expression<Func<TEntity, bool>> SatisfiedBy()
    {
        bool result = true;
        Expression<Func<TEntity, bool>> trueExpression = t => result;
        return trueExpression;
    }
}

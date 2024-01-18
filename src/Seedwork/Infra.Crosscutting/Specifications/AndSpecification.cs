using System;
using System.Linq.Expressions;

namespace Goal.Seedwork.Infra.Crosscutting.Specifications;

public sealed class AndSpecification<TEntity>(ISpecification<TEntity> leftSideSpecification, ISpecification<TEntity> rightSideSpecification) : CompositeSpecification<TEntity>(leftSideSpecification, rightSideSpecification)
    where TEntity : class
{
    public override Expression<Func<TEntity, bool>> SatisfiedBy()
    {
        Expression<Func<TEntity, bool>> left = LeftSideSpecification.SatisfiedBy();
        Expression<Func<TEntity, bool>> right = RightSideSpecification.SatisfiedBy();

        return left.And(right);
    }
}

using System;
using System.Linq.Expressions;

namespace Goal.Infra.Crosscutting.Specifications;

public class DirectSpecification<TEntity> : Specification<TEntity>
    where TEntity : class
{
    private readonly Expression<Func<TEntity, bool>> matchingCriteria;

    public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
    {
        ArgumentNullException.ThrowIfNull(matchingCriteria);
        this.matchingCriteria = matchingCriteria;
    }

    public override Expression<Func<TEntity, bool>> SatisfiedBy() => matchingCriteria;
}

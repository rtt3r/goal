using System;
using System.Linq.Expressions;

namespace Goal.Seedwork.Infra.Crosscutting.Specifications;

public class DirectSpecification<TEntity> : Specification<TEntity>
    where TEntity : class
{
    private readonly Expression<Func<TEntity, bool>> matchingCriteria;

    public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
    {
        Ensure.Argument.IsNotNull(matchingCriteria, nameof(matchingCriteria));
        this.matchingCriteria = matchingCriteria;
    }

    public override Expression<Func<TEntity, bool>> SatisfiedBy() => matchingCriteria;
}

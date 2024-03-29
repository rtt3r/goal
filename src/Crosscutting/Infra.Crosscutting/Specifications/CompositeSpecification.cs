using System;

namespace Goal.Infra.Crosscutting.Specifications;

public abstract class CompositeSpecification<TEntity> : Specification<TEntity>
     where TEntity : class
{
    protected CompositeSpecification(ISpecification<TEntity> leftSideSpecification, ISpecification<TEntity> rightSideSpecification)
    {
        ArgumentNullException.ThrowIfNull(leftSideSpecification);
        ArgumentNullException.ThrowIfNull(rightSideSpecification);

        LeftSideSpecification = leftSideSpecification;
        RightSideSpecification = rightSideSpecification;
    }

    public ISpecification<TEntity> LeftSideSpecification { get; }

    public ISpecification<TEntity> RightSideSpecification { get; }
}

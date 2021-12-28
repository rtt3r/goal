using System;

namespace Goal.Domain.Aggregates
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}

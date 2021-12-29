using System;

namespace Goal.Domain.Seedwork.Aggregates
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}

using System;
using Goal.Seedwork.Domain.Aggregates;

namespace Goal.Seedwork.Application.Tests.Mocks;

internal class EntityTest : Entity<Guid>
{
    public EntityTest()
        : base()
    {
    }

    public EntityTest(Guid id)
        : base()
    {
        Id = id;
    }
}

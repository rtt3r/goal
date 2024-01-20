using System;
using Goal.Domain.Aggregates;

namespace Goal.Application.Tests.Mocks;

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

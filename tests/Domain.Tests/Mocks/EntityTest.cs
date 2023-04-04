using System;

namespace Goal.Seedwork.Domain.Tests.Mocks;

internal class EntityTest : Goal.Seedwork.Domain.Aggregates.Entity
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

    public void SetId(Guid id) => Id = id;
}

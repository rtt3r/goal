using System;
using Goal.Domain.Seedwork.Aggregates;

namespace Goal.Application.Seedwork.Tests.Mocks
{
    internal class EntityTest : Entity
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
}

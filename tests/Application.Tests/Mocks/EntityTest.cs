using System;
using Goal.Seedwork.Domain.Aggregates;

namespace Goal.Seedwork.Application.Tests.Mocks
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

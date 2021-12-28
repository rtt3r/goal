using System;

namespace Goal.Domain.Seedwork.Tests.Mocks
{
    internal class EntityTest : Aggregates.Entity
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

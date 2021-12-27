using Goal.Domain.Aggregates;

namespace Goal.Application.Seedwork.Tests.Mocks
{
    internal class EntityTest : Entity
    {
        public EntityTest()
            : base()
        {
        }

        public EntityTest(int id)
            : base()
        {
            Id = id;
        }

        public void SetId(int id) => Id = id;
    }
}

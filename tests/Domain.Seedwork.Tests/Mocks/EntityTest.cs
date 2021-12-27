namespace Goal.Domain.Seedwork.Tests.Mocks
{
    internal class EntityTest : Aggregates.Entity
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

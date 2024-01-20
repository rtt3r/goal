namespace Goal.Domain.Tests.Mocks;

internal class EntityTest : Goal.Domain.Aggregates.Entity
{
    public EntityTest()
        : base()
    {
    }

    public EntityTest(string id)
        : base()
    {
        Id = id;
    }
}

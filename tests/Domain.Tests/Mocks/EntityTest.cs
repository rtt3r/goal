namespace Goal.Seedwork.Domain.Tests.Mocks;

internal class EntityTest : Goal.Seedwork.Domain.Aggregates.Entity
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

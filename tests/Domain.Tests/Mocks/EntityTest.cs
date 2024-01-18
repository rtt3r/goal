namespace Goal.Seedwork.Domain.Tests.Mocks;

internal class EntityTest : Goal.Domain.Abstractions.Aggregates.Entity
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

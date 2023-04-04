using System.ComponentModel.DataAnnotations;

namespace Goal.Seedwork.Infra.Data.Extensions.RavenDB.Tests.Mocks;

[Display]
internal class TestObject1
{
    public string Id { get; set; }
    public string Value { get; set; }
    public string TestObject2Id { get; set; }
    public TestObject2 TestObject2 { get; set; }

    public TestObject1()
        : base()
    {
    }

    public TestObject1(string id)
        : base()
    {
        Id = id;
    }
}

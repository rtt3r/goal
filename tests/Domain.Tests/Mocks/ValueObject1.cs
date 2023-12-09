using Goal.Seedwork.Domain.Aggregates;

namespace Goal.Seedwork.Domain.Tests.Mocks;

internal class ValueObject1 : ValueObject
{
    public int Id { get; set; }
    public string? Value { get; set; }
}

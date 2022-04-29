using Goal.Seedwork.Domain.Aggregates;

namespace Goal.Seedwork.Domain.Tests.Mocks
{
    internal class ValueObject3 : ValueObject
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public ValueObject3 ValueObject { get; set; }
    }
}

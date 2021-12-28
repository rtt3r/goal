namespace Goal.Domain.Aggregates
{
    public interface IEntity<out TKey> : IEntity
    {
        new TKey Id { get; }
    }
}

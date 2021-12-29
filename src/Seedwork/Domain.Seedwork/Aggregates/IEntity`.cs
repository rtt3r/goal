namespace Goal.Domain.Seedwork.Aggregates
{
    public interface IEntity<out TKey> : IEntity
    {
        new TKey Id { get; }
    }
}

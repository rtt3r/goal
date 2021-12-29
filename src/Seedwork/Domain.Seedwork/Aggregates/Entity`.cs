namespace Goal.Domain.Seedwork.Aggregates
{
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        public new TKey Id { get; protected set; } = default;
    }
}

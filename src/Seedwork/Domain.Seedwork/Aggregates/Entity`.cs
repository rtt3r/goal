namespace Goal.Domain.Aggregates
{
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        public new TKey Id { get; protected set; } = default;
    }
}

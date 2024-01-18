namespace Goal.Domain.Aggregates;

public interface IEntity<out TKey>
{
    TKey Id { get; }
}

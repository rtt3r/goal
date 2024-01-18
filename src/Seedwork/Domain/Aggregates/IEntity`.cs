namespace Goal.Domain.Abstractions.Aggregates;

public interface IEntity<out TKey>
{
    TKey Id { get; }
}

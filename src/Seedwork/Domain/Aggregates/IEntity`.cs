namespace Goal.Seedwork.Domain.Aggregates;

public interface IEntity<out TKey>
{
    TKey Id { get; }
}

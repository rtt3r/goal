namespace Goal.Seedwork.Domain.Aggregates;

public interface IEntity<out TKey>
    where TKey : struct
{
    TKey Id { get; }
}

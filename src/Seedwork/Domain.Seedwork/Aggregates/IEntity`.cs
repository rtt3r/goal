namespace Goal.Domain.Seedwork.Aggregates
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}

namespace Goal.Domain
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}

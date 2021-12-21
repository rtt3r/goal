namespace Vantage.Domain
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }

        bool IsTransient();
    }
}

namespace Vantage.Domain
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}

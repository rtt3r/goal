namespace Vantage.Domain.Business
{
    public interface IBusinessRule<in TEntity>
    {
        void Evaluate(TEntity entity);
    }
}

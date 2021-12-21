namespace Vantage.Domain.Business
{
    public interface IBusinessRulesEvaluator<in TEntity>
    {
        void Evaluate(TEntity entity);
    }
}

using System.Transactions;

namespace Vantage.Application.Services
{
    public abstract class AppService : IAppService
    {
        protected static TransactionScope CreateTransactionScope() => CreateTransactionScope(IsolationLevel.ReadCommitted);

        protected static TransactionScope CreateTransactionScope(IsolationLevel isolationLevel)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = isolationLevel },
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}

using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Goal.Seedwork.Infra.Data.Tests.Mocks;

internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider queryProvider;

    internal TestAsyncQueryProvider(IQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }

    public IQueryable CreateQuery(Expression expression) => new TestAsyncEnumerable<TEntity>(expression);

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestAsyncEnumerable<TElement>(expression);

    public object? Execute(Expression expression) => queryProvider.Execute(expression);

    public TResult Execute<TResult>(Expression expression) => queryProvider.Execute<TResult>(expression);

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default) => Task.Run(() => Execute<TResult>(expression), cancellationToken).GetAwaiter().GetResult();
}

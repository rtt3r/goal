using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vantage.Infra.Data
{
    public interface ISql
    {
        int ExecuteCommand(string sqlCommand, params object[] parameters);
        Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters);
        Task<int> ExecuteCommandAsync(string sqlCommand, IEnumerable<object> parameters, CancellationToken cancellationToken = default);
    }
}

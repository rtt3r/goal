using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Goal.Infra.Data.Seedwork
{
    public interface ISql
    {
        int ExecuteCommand(string sqlCommand, params object[] parameters);
        Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters);
        Task<int> ExecuteCommandAsync(string sqlCommand, IEnumerable<object> parameters, CancellationToken cancellationToken = default);
    }
}

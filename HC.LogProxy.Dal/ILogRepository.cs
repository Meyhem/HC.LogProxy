using System.Threading;
using System.Threading.Tasks;
using HC.LogProxy.Dal.Dto;

namespace HC.LogProxy.Dal
{
    public interface ILogRepository
    {
        Task<LogRecord[]> GetAllLogsAsync(CancellationToken ct);
        Task AddLogsAsync(LogFields[] fields, CancellationToken ct);
    }
}
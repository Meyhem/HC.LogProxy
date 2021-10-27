using System.Threading;
using System.Threading.Tasks;
using HC.LogProxy.Core.Dto;

namespace HC.LogProxy.Core
{
    public interface ILoggingService
    {
        Task CreateLogAsync(CreateLogRecordDto createLogRecordDto, CancellationToken ct);
        Task<LogRecordDto[]> GetLogsAsync(CancellationToken ct);
    }
}
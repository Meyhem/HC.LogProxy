using System.Threading;
using System.Threading.Tasks;
using HC.LogProxy.Core.Dto;

namespace HC.LogProxy.Core
{
    public interface ILoggingService
    {
        Task CreateLog(CreateLogRecordDto createLogRecordDto, CancellationToken ct);
        Task<LogRecordDto[]> GetLogAsync(CancellationToken ct);
    }
}
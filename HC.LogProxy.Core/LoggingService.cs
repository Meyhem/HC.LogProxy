using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HC.LogProxy.Core.Dto;
using HC.LogProxy.Dal;
using HC.LogProxy.Dal.Dto;
using HC.LogProxy.Shared;

namespace HC.LogProxy.Core
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogRepository logRepository;
        private readonly IDateTimeOffsetProvider dateTimeOffsetProvider;
        private readonly IIdentifierProvider identifierProvider;

        public LoggingService(
            ILogRepository logRepository, 
            IDateTimeOffsetProvider dateTimeOffsetProvider, 
            IIdentifierProvider identifierProvider)
        {
            this.logRepository = logRepository;
            this.dateTimeOffsetProvider = dateTimeOffsetProvider;
            this.identifierProvider = identifierProvider;
        }

        public async Task CreateLogAsync(CreateLogRecordDto createLogRecordDto, CancellationToken ct)
        {
            await logRepository.AddLogsAsync(new[]
            {
                new LogFields
                {
                    Fields = LogMapper.IntoFields(
                        createLogRecordDto.Title, 
                        createLogRecordDto.Text,
                        identifierProvider.GetUuid(),
                        dateTimeOffsetProvider.GetUtcNow())
                }
            }, ct);
        }

        public async Task<LogRecordDto[]> GetLogsAsync(CancellationToken ct)
        {
            var logs = await logRepository.GetAllLogsAsync(ct);

            return logs.Select(LogMapper.FromLogRecord).ToArray();
        }
    }
}
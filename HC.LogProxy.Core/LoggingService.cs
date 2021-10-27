﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HC.LogProxy.Core.Dto;
using HC.LogProxy.Dal;
using HC.LogProxy.Dal.Dto;
using HC.LogProxy.Util;

namespace HC.LogProxy.Core
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogRepository logRepository;
        private readonly IDateTimeOffsetProvider dateTimeOffsetProvider;

        public LoggingService(ILogRepository logRepository, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            this.logRepository = logRepository;
            this.dateTimeOffsetProvider = dateTimeOffsetProvider;
        }

        public async Task CreateLog(CreateLogRecordDto createLogRecordDto, CancellationToken ct)
        {
            await logRepository.AddLogsAsync(new[]
            {
                new LogFields
                {
                    Fields = LogMapper.IntoFieldsMap(
                        createLogRecordDto.Title, 
                        createLogRecordDto.Text,
                        Guid.NewGuid().ToString(), 
                        dateTimeOffsetProvider.GetUtcNow())
                }
            }, ct);
        }

        public async Task<LogRecordDto[]> GetLogAsync(CancellationToken ct)
        {
            var logs = await logRepository.GetAllLogsAsync(ct);

            return logs.Select(LogMapper.FromLogRecord).ToArray();
        }
    }
}
using System;
using HC.LogProxy.Core.Dto;
using HC.LogProxy.Dal.Dto;

namespace HC.LogProxy.Core
{
    public static class LogMapper
    {
        public static LogRecordDto FromLogRecord(LogRecord l)
        {
            DateTimeOffset? receivedAt = null;
            
            if (DateTimeOffset.TryParse(l.Fields?.ReceivedAt, out var recv))
            {
                receivedAt = recv;
            }
            
            return new LogRecordDto
            {
                Id = l.Fields?.Id,
                ReceivedAt = receivedAt,
                Text = l.Fields?.Message,
                Title = l.Fields?.Summary
            };
        }

        public static Fields IntoFields(string title, string text, string id, DateTimeOffset receivedAt)
        {
            return new Fields
            {
                Id = id,
                ReceivedAt = receivedAt.ToString("o"),
                Summary = title,
                Message = text,
            };
        }
    }
}
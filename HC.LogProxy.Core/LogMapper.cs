using System;
using System.Collections.Generic;
using HC.LogProxy.Core.Dto;
using HC.LogProxy.Dal.Dto;

namespace HC.LogProxy.Core
{
    public static class LogMapper
    {
        public const string ReceivedAtField = "ReceivedAt";
        public const string IdField = "Id";
        public const string TextField = "Text";
        public const string TitleField = "Title";
        
        public static LogRecordDto FromLogRecord(LogRecord l)
        {
            var receivedAt = l.Fields.GetValueOrDefault(ReceivedAtField, string.Empty);
            DateTimeOffset.TryParse(receivedAt, out var parsedReceivedAt);
            
            return new LogRecordDto
            {
                Id = l.Fields.GetValueOrDefault(IdField, string.Empty),
                ReceivedAt = parsedReceivedAt,
                Text = l.Fields.GetValueOrDefault(TextField, string.Empty),
                Title = l.Fields.GetValueOrDefault(TitleField, string.Empty)
            };
        }

        public static Dictionary<string, string> IntoFieldsMap(string title, string text, string id, DateTimeOffset receivedAt)
        {
            return new Dictionary<string, string>
            {
                [ReceivedAtField] = receivedAt.ToString("o"),
                [IdField] = id,
                [TitleField] = title,
                [TextField] = text,
            };
        }
    }
}
using System;
using System.Collections.Generic;

namespace HC.LogProxy.Dal.Dto
{
    public class GetLogResponse
    {
        public LogRecord[] Records { get; set; }
    }
    
    public class LogRecord
    {
        public string Id { get; set; }
        public Dictionary<string, string> Fields { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}
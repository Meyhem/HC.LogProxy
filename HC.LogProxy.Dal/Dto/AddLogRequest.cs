using System;

namespace HC.LogProxy.Dal.Dto
{
    public class AddLogRequest
    {
        public LogFields[] Records { get; set; } = Array.Empty<LogFields>();
    }

    public class LogFields
    {
        public Fields Fields { get; set; } = new ();
    }
}
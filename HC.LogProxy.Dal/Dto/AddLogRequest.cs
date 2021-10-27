using System.Collections.Generic;

namespace HC.LogProxy.Dal.Dto
{
    public class AddLogRequest
    {
        public LogFields[] Records { get; set; }
    }

    public class LogFields
    {
        public Fields Fields { get; set; }
    }
}
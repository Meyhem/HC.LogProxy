using System;
using System.Collections.Generic;
using HC.LogProxy.Dal.Dto;

namespace HC.LogProxy.Core.Dto
{
    public class LogRecordDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset ReceivedAt { get; set; }
        
        
    }
}
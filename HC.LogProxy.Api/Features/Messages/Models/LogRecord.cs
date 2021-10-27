using System;

namespace HC.LogProxy.Api.Features.Messages.Models
{
    /// <summary>
    /// Represents instance of log record
    /// </summary>
    public class LogRecord
    {
        /// <summary>
        /// Log identifier
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Log title
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Log text
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// Timestamp of reception of this log instance
        /// </summary>
        public DateTimeOffset ReceivedAt { get; set; }
    }
}
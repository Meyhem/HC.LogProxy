using System.ComponentModel.DataAnnotations;

namespace HC.LogProxy.Api.Features.Messages.Models
{
    /// <summary>
    /// Represents request for insertion of new log record
    /// </summary>
    public class CreateLogRecordRequest
    {
        /// <summary>
        /// Title of log record
        /// </summary>
        /// <example>
        /// My log title
        /// </example>
        [Required] public string Title { get; set; } = default!;
        
        /// <summary>
        /// Title of log record
        /// </summary>
        /// <example>
        /// My log text
        /// </example>
        [Required] public string Text { get; set; } = default!;
    }
}
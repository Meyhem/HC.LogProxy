using System.ComponentModel.DataAnnotations;

namespace HC.LogProxy.Api.Models
{
    public class CreateLogRecordRequest
    {
        [Required] public string Title { get; set; } = default!;
        [Required] public string Text { get; set; } = default!;
    }
}
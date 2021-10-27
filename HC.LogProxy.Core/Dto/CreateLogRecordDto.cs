using System.ComponentModel.DataAnnotations;

namespace HC.LogProxy.Core.Dto
{
    public class CreateLogRecordDto
    {
        [Required] public string Title { get; set; } = default!;
        [Required] public string Text { get; set; } = default!;
    }
}
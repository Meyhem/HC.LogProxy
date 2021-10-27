using System.Threading;
using System.Threading.Tasks;
using HC.LogProxy.Api.Models;
using HC.LogProxy.Core;
using HC.LogProxy.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HC.LogProxy.Api.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : BaseController
    {
        private readonly ILoggingService loggingService;

        public MessagesController(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        
        
        [HttpPost("")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateLogRecordRequest request,
            CancellationToken cancellationToken
        )
        {
            await loggingService.CreateLog(new CreateLogRecordDto
            {
                Title = request.Title,
                Text = request.Text
            }, cancellationToken);

            return NoContent();
        }
    }
}
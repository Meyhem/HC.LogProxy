using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HC.LogProxy.Api.Features.Messages.Models;
using HC.LogProxy.Api.Instrumentation;
using HC.LogProxy.Core;
using HC.LogProxy.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HC.LogProxy.Api.Features.Messages
{
    [Route("api/[controller]")]
    public class MessagesController : BaseController
    {
        private readonly ILoggingService loggingService;

        public MessagesController(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        /// <summary>
        /// Gets all log records
        /// </summary>
        /// <param name="cancellationToken">Abort token</param>
        /// <returns>Returns all log records</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(LogRecord[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var logs = (await loggingService.GetLogsAsync(cancellationToken))
                .Select(l => new LogRecord
                {
                    Id = l.Id,
                    Text = l.Text,
                    Title = l.Title,
                    ReceivedAt = l.ReceivedAt
                });

            return Ok(logs);
        }

        /// <summary>
        /// Inserts single log record
        /// </summary>
        /// <param name="request">Insert single log instance</param>
        /// <param name="cancellationToken">Abort token</param>
        /// <returns></returns>
        [HttpPost("")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateLogRecordRequest request,
            CancellationToken cancellationToken)
        {
            await loggingService.CreateLogAsync(new CreateLogRecordDto
            {
                Title = request.Title,
                Text = request.Text
            }, cancellationToken);

            return NoContent();
        }
    }
}
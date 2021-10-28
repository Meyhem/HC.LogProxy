using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HC.LogProxy.Api.Features.Messages.Models;
using HC.LogProxy.Api.Instrumentation;
using HC.LogProxy.Core;
using HC.LogProxy.Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HC.LogProxy.Api.Features.Messages
{
    [Route("api/[controller]")]
    [Authorize]
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
        /// <response code="200">Returns log data</response>
        /// <response code="401">Unauthorized</response>
        /// <param name="cancellationToken">Abort token</param>
        /// <returns>Returns all log records</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(LogRecord[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <response code="204">Log record successfully created</response>
        /// <response code="401">Unauthorized</response>
        /// <param name="request">Insert single log instance</param>
        /// <param name="cancellationToken">Abort token</param>
        /// <returns></returns>
        [HttpPost("")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
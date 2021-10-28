using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace HC.LogProxy.Api.Instrumentation
{
    /// <summary>
    /// Exception interceptor logging and exception and responding with Problem() json
    /// </summary>
    public class ExceptionInterceptor : IExceptionFilter
    {
        private readonly ILogger<ExceptionInterceptor> logger;

        public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            logger.LogError(ex, "Unhandled error");

            var result = new ProblemDetails
            {
                Status = 500,
                Type = ex.GetType().Name,
                Title = ex.Message,
            };

            context.Result = new ObjectResult(result)
            {
                StatusCode = result.Status
            };

            context.ExceptionHandled = true;
        }
    }
}
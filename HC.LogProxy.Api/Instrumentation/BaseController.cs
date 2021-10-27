using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HC.LogProxy.Api.Instrumentation
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Action execution interceptor automatically validating modelstate and responsing with Problem() json if invalid
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!context.ModelState.IsValid)
            {
                var problem = new ProblemDetails
                {
                    Status = 400,
                    Type = "ValidationError",
                    Title = "Failed to validate input data",
                    Detail = ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage
                };
            
                context.Result = new ObjectResult(problem)
                {
                    StatusCode = 400
                };
            }
        }
    }
}
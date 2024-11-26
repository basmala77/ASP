using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD_Operations.Filters
{
    public class LogActfityFilters : IActionFilter
    {
        private readonly ILogger<LogActfityFilters> logger;

        public LogActfityFilters(ILogger<LogActfityFilters> logger) 
        {
            this.logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} finished executed on controller {context.Controller}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} on controller {context.Controller} ");
        }
    }
}

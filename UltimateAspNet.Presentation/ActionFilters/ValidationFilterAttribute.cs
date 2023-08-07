using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UltimateAspNet.Presentation.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var dto = context.ActionArguments
                .SingleOrDefault(x => x.Value?.ToString()?.Contains("Dto", StringComparison.OrdinalIgnoreCase) == true).Value;

            if (dto == null)
            {
                var action = context.RouteData.Values["action"];
                var controller = context.RouteData.Values["controller"];

                context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}");
                return;
            }

            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);

        }
    }
}

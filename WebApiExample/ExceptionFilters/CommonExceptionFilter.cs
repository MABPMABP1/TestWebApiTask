using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiExample.ExceptionFilters
{
    /// <summary>
    /// Common exceptions filter. Handles unhandled exceptionss in common way
    /// </summary>
    public class CommonExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception != null)
            {
                context.Result = ActionResultsGenerator.GetCommonFailureActionResult($"Ошибка сервера: <{context.Exception.Message}>");
                context.ExceptionHandled = true;
            }
        }
    }
}

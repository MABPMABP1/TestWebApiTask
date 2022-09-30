using Microsoft.AspNetCore.Mvc;
using WebApiExample.Responses.Error;

namespace WebApiExample
{
    /// <summary>
    /// Default responses generator
    /// </summary>
    public static class ActionResultsGenerator
    {
        public static IActionResult GetUnauthorizedActionResult() =>
            new ObjectResult(new UnauthorizedErrorResponse())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        public static IActionResult GetTokenExpiredActionResult() =>
            new ObjectResult(new TokenExpiredErrorResponce())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        public static IActionResult GetCommonFailureActionResult(string message) =>
            new ObjectResult(new ErrorResponse(message))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
    }
}

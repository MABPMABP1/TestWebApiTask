using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using WebApiExample.Exceptions;
using WebApiExample.Services.TokenServices;

namespace WebApiExample.Filters
{
    /// <summary>
    /// Authorization filter. Implements base token checking logic. 
    /// </summary>
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IActionResult result = null;

            try
            {
                bool isAuthorized = false;
                string authHeaderName = HeaderNames.Authorization;
                StringValues authHeaderValues;
                bool isTokenFound = context.HttpContext.Request.Headers.TryGetValue(authHeaderName, out authHeaderValues);

                if (isTokenFound)
                {
                    string token = authHeaderValues.First();
                    ITokensService tokenService = (ITokensService)context.HttpContext.RequestServices.GetService(typeof(ITokensService));

                    Task<bool> task = tokenService.ValidateToken(token);
                    task.Wait();
                    isAuthorized = task.Result;
                }

                if (!isAuthorized)
                {
                    result = ActionResultsGenerator.GetUnauthorizedActionResult();
                }
            }
            catch(AggregateException ex)
            {
                if(ex.InnerExceptions.Count > 1)
                {
                    // Uncommon situation with multiple exceptions. Logic should be extetnded for such cases
                    result = ActionResultsGenerator.GetCommonFailureActionResult(ex.Message);
                }
                else
                {
                    switch (ex.InnerException) {
                        case TokenExpiredException e:
                            result = ActionResultsGenerator.GetTokenExpiredActionResult();
                            break;
                        case TokenNotFoundException e:
                            result = ActionResultsGenerator.GetUnauthorizedActionResult();
                            break;
                        case Exception e:
                            result = ActionResultsGenerator.GetCommonFailureActionResult(ex.Message);
                            break;
                    };
                }
            }
            catch (Exception ex)
            {
                result = ActionResultsGenerator.GetCommonFailureActionResult(ex.Message);
            }

            if(result != null)
            {
                context.Result = result;
            }
        }
    }
}

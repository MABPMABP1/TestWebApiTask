using Microsoft.AspNetCore.Mvc;
using WebApiExample.Requests.Auth;
using WebApiExample.Responses.Auth;
using WebApiExample.Responses.Error;
using WebApiExample.Services.CredentialsServices;
using WebApiExample.Services.TokenServices;

namespace WebApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string _tokenLifetimeConfigKey = "TokenLifetime";

        private readonly IConfiguration _configuration;
        private readonly ICredentialsService _credentialsService;
        private readonly ITokensService _tokensService;

        public AuthController(IConfiguration configuration, ICredentialsService credentialsService, ITokensService tokensService)
        {
            _configuration = configuration;
            _credentialsService = credentialsService;
            _tokensService = tokensService;
        }

        [HttpPost("getToken")]
        public async Task<IActionResult> GetToken(AuthorizationRequest request)
        {
            bool areCredsValid = await _credentialsService.CheckCredentials(request.Login, request.Password);
            if (areCredsValid)
            {
                int tokenLifetime = _configuration.GetValue<int>(_tokenLifetimeConfigKey);
                string token = await _tokensService.IssueToken(request.Login, tokenLifetime);
                return Ok(new AuthResponse { Success = true, Token = token});
            }

            return StatusCode(StatusCodes.Status403Forbidden, new UnauthorizedErrorResponse());
        }
    }
}

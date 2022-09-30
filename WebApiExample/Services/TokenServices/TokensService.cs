using WebApiExample.Services.TokenServices.TokenManagers;
using WebApiExample.Services.TokenServices.TokenProviders;

namespace WebApiExample.Services.TokenServices
{
    /// <summary>
    /// Base implementation of <see cref="ITokensService"/>. 
    /// Contains separate token provider and token manager for separating generating and storage logic
    /// </summary>
    public class TokensService : ITokensService
    {
        private const int _defaultTokenLifeTimeInSeconds = 7200;

        protected readonly ITokenProvider _tokenProvider;
        protected readonly ITokenManager _tokenManager;

        public TokensService(ITokenProvider tokenProvider, ITokenManager tokenManager)
        {
            _tokenProvider = tokenProvider;
            _tokenManager = tokenManager;
        }

        public async Task<string> IssueToken(string owner) => await IssueToken(owner, _defaultTokenLifeTimeInSeconds);

        public async Task<string> IssueToken(string owner, int tokenLifetime)
        {
            AuthToken token = await _tokenProvider.IssueToken(tokenLifetime);
            await _tokenManager.SaveToken(owner, token);
            return token.Token;
        }

        public async Task<bool> ValidateToken(string token)
        {
            return await _tokenManager.ValidateToken(token);
        }
    }
}

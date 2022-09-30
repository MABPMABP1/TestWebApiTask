namespace WebApiExample.Services.TokenServices.TokenProviders
{
    /// <summary>
    /// Base token provider interface. Token provider is responsible only for issuing new tokens
    /// </summary>
    public interface ITokenProvider
    {
        /// <summary>
        /// Issue token with default parameters
        /// </summary>
        /// <returns>New token</returns>
        public Task<AuthToken> IssueToken();

        /// <summary>
        /// Issue token with specified lifetime
        /// </summary>
        /// <param name="lifetimeInSeconds">Lifetime in secondss</param>
        /// <returns>New token</returns>
        public Task<AuthToken> IssueToken(int lifetimeInSeconds);
    }
}

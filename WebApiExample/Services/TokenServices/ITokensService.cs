namespace WebApiExample.Services.TokenServices
{
    /// <summary>
    /// Base interface for token services
    /// </summary>
    public interface ITokensService
    {
        /// <summary>
        /// Issues new token for passsed owner
        /// </summary>
        /// <param name="owner">Owner id</param>
        /// <returns>New token</returns>
        public Task<string> IssueToken(string owner);

        /// <summary>
        /// Issues new token for passsed owner with specified lifetime
        /// </summary>
        /// <param name="owner">Owner id</param>
        /// <param name="tokenLifetime">Token lifetime</param>
        /// <returns>New token</returns>
        public Task<string> IssueToken(string owner, int tokenLifetime);
        
        /// <summary>
        /// Validates token
        /// </summary>
        /// <param name="token">Token to validate</param>
        /// <returns>True for valid token. Otherwise false</returns>
        public Task<bool> ValidateToken(string token);
    }
}

namespace WebApiExample.Services.TokenServices.TokenManagers
{
    /// <summary>
    /// base interface for token managers. Contains only token managing logic.
    /// </summary>
    public interface ITokenManager
    {
        /// <summary>
        /// Saves token for owner
        /// </summary>
        /// <param name="owner">Token owner</param>
        /// <param name="token">Token itself</param>
        public Task SaveToken(string owner, AuthToken token);
        
        /// <summary>
        /// Validates token
        /// </summary>
        /// <param name="token">Token to validate</param>
        /// <returns>True for valid token. Otherwise false</returns>
        public Task<bool> ValidateToken(string token);
        
        /// <summary>
        /// Removes expired tokens from storage.
        /// </summary>
        public Task CleanupExpiredTokens();
    }
}

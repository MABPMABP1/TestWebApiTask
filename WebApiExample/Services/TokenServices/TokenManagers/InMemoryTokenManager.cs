using WebApiExample.Exceptions;

namespace WebApiExample.Services.TokenServices.TokenManagers
{
    /// <summary>
    /// In memory <see cref="ITokenManager"/> implementations.
    /// Stores tokens and owners in memory. 
    /// Contains self-cleanup logic bassed on number executed requests
    /// No external calls. Delays emulated by <see cref="TaskWorkEmulator"/>
    /// </summary>
    public class InMemoryTokenManager: ITokenManager
    {
        private const int _defaultCleanupInterval = 10;

        private readonly Dictionary<string, AuthToken> _authTokens;
        private readonly Dictionary<string, AuthToken> _tokenOwners;
        private readonly int _cleanupInterval;

        private int _requestsCount;

        public InMemoryTokenManager(int cleanupTokensInterval = _defaultCleanupInterval)
        {
            if(cleanupTokensInterval <= 0)
            {
                cleanupTokensInterval = _defaultCleanupInterval;
            }
            _cleanupInterval = cleanupTokensInterval;
            _authTokens = new Dictionary<string, AuthToken>();
            _tokenOwners = new Dictionary<string, AuthToken>();
            _requestsCount = 0;

            InsertTestData();
        }

        /// <summary>
        /// Insert test data in memory. Required for test purposes.
        /// </summary>
        private void InsertTestData()
        {
            AuthToken validToken = new AuthToken
            {
                Token = "11111111-1111-1111-1111-111111111111",
                ExpirationDate = DateTime.UtcNow.AddDays(1)
                
            };
            _authTokens.Add(validToken.Token, validToken);
            _tokenOwners.Add("Default user", validToken);


            AuthToken expiredToken = new AuthToken
            {
                Token = "00000000-0000-0000-0000-000000000000",
                ExpirationDate = DateTime.UtcNow.AddDays(-1)

            };
            _authTokens.Add(expiredToken.Token, expiredToken);
            _tokenOwners.Add("Another user", expiredToken);
        }

        /// <summary>
        /// Registers request execution
        /// </summary>
        private void RegisterRequest() => _requestsCount++;
        private bool CleanupRequired() => _requestsCount >= _cleanupInterval;
        
        private bool IsTokenExpired(AuthToken token, DateTime expirationDate) => 
            DateTime.Compare(token.ExpirationDate, expirationDate) <= 0;

        /// <summary>
        /// Removes all expired tokens only if cleanup is required. 
        /// Decision about cleaning up based on executed requests count
        /// </summary>
        /// <returns></returns>
        public async Task CleanupExpiredTokens()
        {
            if (!CleanupRequired())
                return;

            DateTime expirationDate = DateTime.Now;

            await TaskWorkEmulator.DoWork();

            var expiredTokens = 
                _authTokens.Values.Where(token => IsTokenExpired(token, expirationDate));
            foreach(var token in expiredTokens)
            {
                _authTokens.Remove(token.Token);
            }
        }

        /// <summary>
        /// Registers request and save token for owner. 
        /// Replaces info about owning token with new one for cases when owner already has been registered with another token
        /// </summary>
        /// <param name="owner">Token owner</param>
        /// <param name="token">Token itself</param>
        /// <returns></returns>
        public async Task SaveToken(string owner, AuthToken token)
        {
            RegisterRequest();

            await TaskWorkEmulator.DoWork();

            AuthToken oldToken;
            if(_tokenOwners.TryGetValue(owner, out oldToken))
            {
                _authTokens.Remove(oldToken.Token);
            }

            _tokenOwners[owner] = token;
            _authTokens[token.Token] = token;

            await CleanupExpiredTokens();
        }


        /// <summary>
        /// Validates token
        /// </summary>
        /// <param name="token">Token to validate</param>
        /// <returns></returns>
        /// <exception cref="TokenExpiredException">Thrown for expired tokens</exception>
        /// <exception cref="TokenNotFoundException">Thrown for not found tokens</exception>
        public async Task<bool> ValidateToken(string token)
        {
            RegisterRequest();

            await TaskWorkEmulator.DoWork();
            await CleanupExpiredTokens();

            AuthToken storedToken;
            if(_authTokens.TryGetValue(token, out storedToken))
            {
                if(IsTokenExpired(storedToken, DateTime.UtcNow))
                    throw new TokenExpiredException();
                return true;
            }

            throw new TokenNotFoundException();
        }
    }
}

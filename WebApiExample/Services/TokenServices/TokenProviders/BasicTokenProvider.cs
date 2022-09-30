namespace WebApiExample.Services.TokenServices.TokenProviders
{
    /// <summary>
    /// Basic <see cref="ITokenProvider"/> implementation. Uses Guid as tokens.
    /// No external calls. Delays emulated by <see cref="TaskWorkEmulator"/>
    /// </summary>
    public class BasicTokenProvider : ITokenProvider
    {
        private const int _defaultTokenLifeTimeInSeconds = 7200;

        public async Task<AuthToken> IssueToken() => await IssueToken(_defaultTokenLifeTimeInSeconds);
        public async Task<AuthToken> IssueToken(int lifetimeInSeconds)
        {
            await TaskWorkEmulator.DoWork();
            return new AuthToken {
                Token = Guid.NewGuid().ToString(),
                ExpirationDate = DateTime.UtcNow.AddSeconds(lifetimeInSeconds)
            };
        }
    }
}

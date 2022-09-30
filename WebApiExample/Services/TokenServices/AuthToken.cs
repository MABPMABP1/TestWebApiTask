namespace WebApiExample.Services.TokenServices
{
    /// <summary>
    /// Basse token information structure
    /// </summary>
    public class AuthToken
    {
        /// <summary>
        /// Token itself
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token expiration date in UTC
        /// </summary>
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow;
    }
}

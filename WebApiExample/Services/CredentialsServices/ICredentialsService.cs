namespace WebApiExample.Services.CredentialsServices
{
    /// <summary>
    /// Base interface for credential services
    /// </summary>
    public interface ICredentialsService
    {
        /// <summary>
        /// Checks if provided credentials are valid
        /// </summary>
        /// <param name="login">User logic</param>
        /// <param name="password">User passsword</param>
        /// <returns></returns>
        public Task<bool> CheckCredentials(string login, string password);
    }
}

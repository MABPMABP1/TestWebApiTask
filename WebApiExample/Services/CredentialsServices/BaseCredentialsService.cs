namespace WebApiExample.Services.CredentialsServices
{
    /// <summary>
    /// Basic implementation of <see cref="ICredentialsService"/>
    /// Abstract method should be implemented in child clasess
    /// </summary>
    public abstract class BaseCredentialsService : ICredentialsService
    {
        /// <summary>
        /// Provides stored user secret for comparing with password. It could be paswword itself, hash, etc.
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>User's secret</returns>
        protected abstract Task<string> GetSecret(string login);

        /// <summary>
        /// Checks if password corresonds to user secret.
        /// </summary>
        /// <param name="password">User passsword to check</param>
        /// <param name="secret">Stored secret to chech</param>
        /// <returns>True if user secret correspons to entered password/ Otherwise false</returns>
        protected abstract bool IsEqualToSecret(string password, string secret);

        /// <summary>
        /// Basic login validation logic
        /// </summary>
        /// <param name="login">Login to validate</param>
        /// <returns>True for valid login. Otherwise false</returns>
        protected bool IsLoginValid(string login) => !String.IsNullOrWhiteSpace(login);

        /// <summary>
        /// Basic password validation logic
        /// </summary>
        /// <param name="password">Password to validate</param>
        /// <returns>True for valid passsword. Othervise - false</returns>
        protected bool IsPasswordValid(string password) => !String.IsNullOrWhiteSpace(password);

        public async Task<bool> CheckCredentials(string login, string password)
        {
            if(IsLoginValid(login) && IsPasswordValid(password))
            {
                string secret = await GetSecret(login);
                return IsEqualToSecret(password, secret);
            }
            return false;
        }
    }
}

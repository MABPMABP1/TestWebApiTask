using WebApiExample.Exceptions;

namespace WebApiExample.Services.CredentialsServices
{
    /// <summary>
    /// In memory credentials service implementation. All data stored inside instance of this class.
    /// No external calls. Delays emulated by <see cref="TaskWorkEmulator"/>
    /// Contains test predefined data
    /// </summary>
    public class InMemoryCredentialsService : BaseCredentialsService
    {
        private Dictionary<string, string> _credentials = new Dictionary<string, string>
        {
            {"Admin", "StrongPassword" },
            {"Bad Admin", "Admin123" },
            {"Default user", "password" },
            {"Another user", "qwertyPass" }
        };

        /// <summary>
        /// Gets user secret from memory
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>User secret</returns>
        /// <exception cref="UserSecretNotFoundException">Thrown when secret can not be found</exception>
        protected override async Task<string> GetSecret(string login)
        {
            await TaskWorkEmulator.DoWork();

            string secret;
            if (_credentials.TryGetValue(login, out secret))
            {
                return secret;
            }

            throw new UserSecretNotFoundException();
        }

        /// <summary>
        /// Compares secret with passsword itsself. 
        /// No additional logic for convertinf user passsword to some kind of secret
        /// </summary>
        /// <param name="password">User password</param>
        /// <param name="secret">User secret</param>
        /// <returns>True for equal passsword and secret. Otherwise false</returns>
        protected override bool IsEqualToSecret(string password, string secret) => String.Equals(password, secret);
    }
}

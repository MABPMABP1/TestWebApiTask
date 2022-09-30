using WebApiExample.Exceptions;

namespace WebApiExample.Services.CredentialsServices
{
    /// <summary>
    /// Credentials service implementation based on 'appsettings.json' file.
    /// No external calls. Delays emulated by <see cref="TaskWorkEmulator"/>
    /// </summary>
    public class AppSettingsBasedCredentialsService: BaseCredentialsService
    {
        private const string _storageConfigurationKey = "Credentials";

        protected readonly IConfiguration _configuration;

        public AppSettingsBasedCredentialsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets user secret from configuration file.
        /// </summary>
        /// <param name="login">user login</param>
        /// <returns>User secret</returns>
        /// <exception cref="UserSecretNotFoundException">Thrown when secret can not be found</exception>
        protected override async Task<string> GetSecret(string login)
        {
            await TaskWorkEmulator.DoWork();

            IConfigurationSection credsSection = _configuration.GetSection(_storageConfigurationKey);
            var creds = credsSection.Get <Dictionary<string, string>>();

            string secret;
            if (creds.TryGetValue(login, out secret))
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

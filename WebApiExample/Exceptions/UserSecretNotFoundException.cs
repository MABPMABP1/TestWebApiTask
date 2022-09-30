namespace WebApiExample.Exceptions
{
    /// <summary>
    /// Specific exception signaling about not found user secret
    /// </summary>
    public class UserSecretNotFoundException: Exception
    {
        public UserSecretNotFoundException(): base() {}
    }
}

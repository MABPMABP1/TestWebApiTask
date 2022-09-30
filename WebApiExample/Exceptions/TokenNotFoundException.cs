namespace WebApiExample.Exceptions
{
    /// <summary>
    /// Specific exception signaling about missing token
    /// </summary>
    public class TokenNotFoundException: Exception
    {
        public TokenNotFoundException() : base() { }
    }
}

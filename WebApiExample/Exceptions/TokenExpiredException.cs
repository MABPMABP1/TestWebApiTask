namespace WebApiExample.Exceptions
{
    /// <summary>
    /// Specific exception signales about expired token found durinf authorization process
    /// </summary>
    public class TokenExpiredException: Exception
    {
        public TokenExpiredException() : base() { }
    }
}

namespace WebApiExample.Responses.Error
{
    public class TokenExpiredErrorResponce: ErrorResponse
    {
        private const string _defaultErrorMessage = "Expired";

        public TokenExpiredErrorResponce() : base(_defaultErrorMessage, false) { }
    }
}

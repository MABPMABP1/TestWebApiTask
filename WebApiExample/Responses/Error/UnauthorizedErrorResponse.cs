namespace WebApiExample.Responses.Error
{
    public class UnauthorizedErrorResponse: ErrorResponse
    {
        private const string _defaultErrorMessage = "Unauthorized";
        public UnauthorizedErrorResponse() : base(_defaultErrorMessage, false) { }
    }
}

namespace WebApiExample.Responses.Error
{
    public class ErrorResponse: BaseResponse
    {
        public ErrorResponse(string message, bool success = false) : base(success)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}

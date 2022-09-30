namespace WebApiExample.Responses
{
    public abstract class BaseResponse
    {
        public BaseResponse(bool success = false)
        {
            Success = success;
        }
        public bool Success { get; set; }
    }
}

namespace WebApiExample.Responses.Data
{
    public class DataResponse:BaseResponse
    {
        public string  Data1 { get; set; }
        public string Data2 { get; set; }

        private string GetDataMessage(int delay) => $"меня получали {delay} секунд";

        public DataResponse(int delay1, int delay2):base(true)
        {
            Data1 = GetDataMessage(delay1);
            Data2 = GetDataMessage(delay2);
        }
    }
}

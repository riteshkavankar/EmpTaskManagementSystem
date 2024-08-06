namespace employeetaskapis.Models.Dto
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}

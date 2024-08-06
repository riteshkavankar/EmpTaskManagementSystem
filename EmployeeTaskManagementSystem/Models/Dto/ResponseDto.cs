namespace EmployeeTaskManagementSystem.Models.Dto
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class DataContainer<T>
    {
        public List<T> Values { get; set; } = new List<T>();
    }

}

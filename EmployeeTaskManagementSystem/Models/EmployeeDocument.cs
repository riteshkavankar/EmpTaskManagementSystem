namespace EmployeeTaskManagementSystem.Models
{
    public class EmployeeDocument
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public int TaskId { get; set; }
        public EmployeeTask Task { get; set; }
    }

}

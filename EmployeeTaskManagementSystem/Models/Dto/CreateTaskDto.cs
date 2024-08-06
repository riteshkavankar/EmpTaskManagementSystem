namespace EmployeeTaskManagementSystem.Models.Dto
{
    public class CreateTaskDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
    }
}

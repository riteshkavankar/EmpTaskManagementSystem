using System.Reflection.Metadata;

namespace employeetaskapis.Models
{
    public class EmployeeTask
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public List<Note> Notes { get; set; }
        public List<EmployeeDocument> Documents { get; set; }
    }
}

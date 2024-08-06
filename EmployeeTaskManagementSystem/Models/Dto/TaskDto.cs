namespace EmployeeTaskManagementSystem.Models.Dto
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
        public List<CreateNoteDto> Notes { get; set; }
        public List<CreateEmployeeDocumentDto> Documents { get; set; }
    }

    public class NotesWrapper
    {
        public List<CreateNoteDto> values { get; set; }
    }

    public class DocumentsWrapper
    {
        public List<CreateEmployeeDocumentDto> values { get; set; }
    }
}

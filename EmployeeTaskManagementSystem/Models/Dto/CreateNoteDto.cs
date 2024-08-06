namespace EmployeeTaskManagementSystem.Models.Dto
{
    public class CreateNoteDto
    {
        public int NoteId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TaskId { get; set; }
    }
}

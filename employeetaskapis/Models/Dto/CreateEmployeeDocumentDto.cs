namespace employeetaskapis.Models.Dto
{
    public class CreateEmployeeDocumentDto
    {
        public string FileName { get; set; }
        public IFormFile File { get; set; } 
        public int TaskId { get; set; }
    }
}

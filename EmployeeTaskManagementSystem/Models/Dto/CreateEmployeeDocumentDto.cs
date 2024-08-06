using Microsoft.AspNetCore.Http;

namespace EmployeeTaskManagementSystem.Models.Dto
{
    public class CreateEmployeeDocumentDto
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public int TaskId { get; set; }
        public IFormFile File { get; set; }
        public byte[] FileContent { get; set; }

    }

}

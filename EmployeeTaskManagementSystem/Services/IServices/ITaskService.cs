using EmployeeTaskManagementSystem.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Service
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> GetTaskByIdAsync(int id);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);
        Task<CreateNoteDto> AddNoteToTaskAsync(int taskId, CreateNoteDto createNoteDto);
        Task<CreateEmployeeDocumentDto> AddDocumentToTaskAsync(int taskId, CreateEmployeeDocumentDto createDocumentDto);
        Task DeleteNoteFromTaskAsync(int noteId);
        Task DeleteDocumentFromTaskAsync(int documentId);
        Task<CreateEmployeeDocumentDto> GetDocumentByIdAsync(int documentId);
    }
}

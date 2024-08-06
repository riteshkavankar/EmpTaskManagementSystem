using EmployeeTaskManagementSystem.Models.Dto;
using EmployeeTaskManagementSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return Ok(task);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskDto createTaskDto)
        {
            var task = await _taskService.CreateTaskAsync(createTaskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.TaskId }, task);
        }

        [HttpPost("{taskId}/add-note")]
        public async Task<ActionResult<CreateNoteDto>> AddNoteToTask(int taskId, CreateNoteDto createNoteDto)
        {
            var note = await _taskService.AddNoteToTaskAsync(taskId, createNoteDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = taskId }, note);
        }

        [HttpPost("{taskId}/add-document")]
        public async Task<ActionResult<CreateEmployeeDocumentDto>> AddDocumentToTask(int taskId, CreateEmployeeDocumentDto createDocumentDto)
        {
            var document = await _taskService.AddDocumentToTaskAsync(taskId, createDocumentDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = taskId }, document);
        }

        [HttpDelete("delete-note/{noteId}")]
        public async Task<IActionResult> DeleteNoteFromTask(int noteId)
        {
            await _taskService.DeleteNoteFromTaskAsync(noteId);
            return NoContent();
        }

        [HttpDelete("delete-document/{documentId}")]
        public async Task<IActionResult> DeleteDocumentFromTask(int documentId)
        {
            await _taskService.DeleteDocumentFromTaskAsync(documentId);
            return NoContent();
        }
    }
}

using EmployeeTaskManagementSystem.Models.Dto;
using EmployeeTaskManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Controllers
{
    public class TaskDetailsController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskDetailsController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                return View(task);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(int taskId, string content)
        {
            var createNoteDto = new CreateNoteDto { Content = content, TaskId = taskId };
            await _taskService.AddNoteToTaskAsync(taskId, createNoteDto);
            return RedirectToAction("Index", new { id = taskId });
        }

        [HttpPost]
        public async Task<IActionResult> AddDocument(int taskId, string fileName, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var createDocumentDto = new CreateEmployeeDocumentDto
            {
                FileName = fileName,
                TaskId = taskId,
                File = file
            };

            try
            {
                await _taskService.AddDocumentToTaskAsync(taskId, createDocumentDto);
                return RedirectToAction("Index", new { id = taskId });
            }
            catch (Exception ex)
            {
                // Handle exception (log it, show error message, etc.)
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteNote(int noteId, int taskId)
        {
            await _taskService.DeleteNoteFromTaskAsync(noteId);
            return RedirectToAction("Index", new { id = taskId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDocument(int documentId, int taskId)
        {
            await _taskService.DeleteDocumentFromTaskAsync(documentId);
            return RedirectToAction("Index", new { id = taskId });
        }

        [HttpGet]
        public async Task<IActionResult> DownloadDocument(int documentId)
        {
            try
            {
                var document = await _taskService.GetDocumentByIdAsync(documentId);
                if (document == null)
                {
                    return NotFound();
                }

                var contentDisposition = new System.Net.Mime.ContentDisposition
                {
                    FileName = document.FileName,
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

                return File(document.FileContent, "application/octet-stream", document.FileName);
            }
            catch (Exception ex)
            {
                // Log the exception (use a logger in a real application)
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}

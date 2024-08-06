using employeetaskapis.Data;
using employeetaskapis.Models.Dto;
using employeetaskapis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employeetaskapis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto createTaskDto)
        {
            var task = new EmployeeTask
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                DueDate = createTaskDto.DueDate,
                Status = createTaskDto.Status,
                EmployeeId = createTaskDto.EmployeeId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Notes)
                .Include(t => t.Documents)
                .FirstOrDefaultAsync(t => t.TaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost("{taskId}/add-note")]
        public async Task<IActionResult> AddNoteToTask(int taskId, CreateNoteDto addNoteDto)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            var note = new Note
            {
                Content = addNoteDto.Content,
                TaskId = taskId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return Ok(note);
        }

        [HttpPost("{taskId}/documents")]
        public async Task<IActionResult> AddDocumentToTask(int taskId, [FromForm] CreateEmployeeDocumentDto addDocumentDto)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            if (addDocumentDto.File != null && addDocumentDto.File.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await addDocumentDto.File.CopyToAsync(memoryStream);
                    var extension = Path.GetExtension(addDocumentDto.File.FileName); 

                    var document = new EmployeeDocument
                    {
                        FileName = addDocumentDto.FileName + extension, 
                        FileContent = memoryStream.ToArray(),
                        TaskId = taskId
                    };

                    _context.Documents.Add(document);
                    await _context.SaveChangesAsync();

                    return Ok(document);
                }
            }
            return BadRequest("No file uploaded");
        }


        [HttpDelete("delete-note/{noteId}")]
        public async Task<IActionResult> DeleteNoteFromTask(int noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-document/{documentId}")]
        public async Task<IActionResult> DeleteDocumentFromTask(int documentId)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("download-document/{documentId}")]
        public async Task<IActionResult> DownloadDocument(int documentId)
        {
            try
            {
                var document = await _context.Documents.FindAsync(documentId);
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
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}

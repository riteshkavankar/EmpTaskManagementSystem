using EmployeeTaskManagementSystem.Models.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Service
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TaskDto>>("api/tasks");
        }

        public async Task<TaskDto> GetTaskByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/tasks/{id}");
                response.EnsureSuccessStatusCode();
                var rawContent = await response.Content.ReadAsStringAsync();

                TaskDto taskDto = null;

                using (JsonDocument doc = JsonDocument.Parse(rawContent))
                {
                    JsonElement root = doc.RootElement;

                    taskDto = new TaskDto
                    {
                        TaskId = root.GetProperty("taskId").GetInt32(),
                        Title = root.GetProperty("title").GetString(),
                        Description = root.GetProperty("description").GetString(),
                        DueDate = root.GetProperty("dueDate").GetDateTime(),
                        Status = root.GetProperty("status").GetString(),
                        EmployeeId = root.GetProperty("employeeId").GetInt32(),
                        Notes = new List<CreateNoteDto>(),
                        Documents = new List<CreateEmployeeDocumentDto>()
                    };

                    // Parse notes
                    JsonElement notesElement = root.GetProperty("notes");
                    if (notesElement.TryGetProperty("$values", out JsonElement notesValues))
                    {
                        foreach (JsonElement note in notesValues.EnumerateArray())
                        {
                            taskDto.Notes.Add(new CreateNoteDto
                            {
                                NoteId = note.GetProperty("noteId").GetInt32(),
                                Content = note.GetProperty("content").GetString(),
                                CreatedAt = note.GetProperty("createdAt").GetDateTime(),
                                TaskId = note.GetProperty("taskId").GetInt32()
                            });
                        }
                    }

                    // Parse documents
                    JsonElement documentsElement = root.GetProperty("documents");
                    if (documentsElement.TryGetProperty("$values", out JsonElement documentsValues))
                    {
                        foreach (JsonElement document in documentsValues.EnumerateArray())
                        {
                            taskDto.Documents.Add(new CreateEmployeeDocumentDto
                            {
                                DocumentId = document.GetProperty("documentId").GetInt32(),
                                FileName = document.GetProperty("fileName").GetString(),
                                FileContent = document.GetProperty("fileContent").GetBytesFromBase64(), // Assuming fileContent is a base64 string
                                TaskId = document.GetProperty("taskId").GetInt32()
                            });
                        }
                    }
                }

                return taskDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tasks", createTaskDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskDto>();
        }

        public async Task<CreateNoteDto> AddNoteToTaskAsync(int taskId, CreateNoteDto createNoteDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/tasks/{taskId}/add-note", createNoteDto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CreateNoteDto>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CreateEmployeeDocumentDto> AddDocumentToTaskAsync(int taskId, CreateEmployeeDocumentDto createDocumentDto)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(createDocumentDto.FileName), "FileName");
            formData.Add(new StringContent(createDocumentDto.TaskId.ToString()), "TaskId");

            if (createDocumentDto.File != null)
            {
                formData.Add(new StreamContent(createDocumentDto.File.OpenReadStream()), "File", createDocumentDto.File.FileName);
            }

            var response = await _httpClient.PostAsync($"api/tasks/{taskId}/documents", formData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CreateEmployeeDocumentDto>();
        }

        public async Task DeleteNoteFromTaskAsync(int noteId)
        {
            var response = await _httpClient.DeleteAsync($"api/tasks/delete-note/{noteId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteDocumentFromTaskAsync(int documentId)
        {
            var response = await _httpClient.DeleteAsync($"api/tasks/delete-document/{documentId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<CreateEmployeeDocumentDto> GetDocumentByIdAsync(int documentId)
        {
            var response = await _httpClient.GetAsync($"api/tasks/download-document/{documentId}");
            response.EnsureSuccessStatusCode();

            var fileContent = await response.Content.ReadAsByteArrayAsync();
            var fileName = response.Content.Headers.ContentDisposition?.FileName?.Trim('"');

            var document = new CreateEmployeeDocumentDto
            {
                DocumentId = documentId,
                FileName = fileName,
                FileContent = fileContent
            };

            return document;
        }
    }
}

using EmployeeTaskManagementSystem.Models.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("api/employees");
            response.EnsureSuccessStatusCode();
            var rawContent = await response.Content.ReadAsStringAsync();

            var employees = new List<EmployeeDto>();
            using (JsonDocument doc = JsonDocument.Parse(rawContent))
            {
                JsonElement root = doc.RootElement;
                JsonElement data = root.GetProperty("data");
                JsonElement values = data.GetProperty("$values");

                foreach (JsonElement employee in values.EnumerateArray())
                {
                    var employeeDto = new EmployeeDto
                    {
                        EmployeeId = employee.GetProperty("employeeId").GetInt32(),
                        Name = employee.GetProperty("name").GetString(),
                        Email = employee.GetProperty("email").GetString(),
                        Tasks = new List<TaskDto>()
                    };

                    // Fetch tasks for this employee
                    var tasksResponse = await _httpClient.GetAsync($"api/employees/{employeeDto.EmployeeId}/tasks");
                    tasksResponse.EnsureSuccessStatusCode();
                    var tasksContent = await tasksResponse.Content.ReadAsStringAsync();

                    using (JsonDocument taskDoc = JsonDocument.Parse(tasksContent))
                    {
                        JsonElement taskRoot = taskDoc.RootElement;
                        JsonElement taskData = taskRoot.GetProperty("data");
                        JsonElement taskValues = taskData.GetProperty("$values");

                        foreach (JsonElement task in taskValues.EnumerateArray())
                        {
                            var taskDto = new TaskDto
                            {
                                TaskId = task.GetProperty("taskId").GetInt32(),
                                Title = task.GetProperty("title").GetString(),
                                DueDate = task.GetProperty("dueDate").GetDateTime()
                            };

                            employeeDto.Tasks.Add(taskDto);
                        }
                    }

                    employees.Add(employeeDto);
                }
            }

            return employees;
        }


        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeDto>($"api/employees/{id}");
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/employees", createEmployeeDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmployeeDto>();
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByEmployeeIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/employees/{id}/tasks");
            response.EnsureSuccessStatusCode();

            var rawContent = await response.Content.ReadAsStringAsync();
            var tasks = new List<TaskDto>();

            using (JsonDocument doc = JsonDocument.Parse(rawContent))
            {
                JsonElement root = doc.RootElement;
                JsonElement data = root.GetProperty("data");
                JsonElement values = data.GetProperty("$values");

                foreach (JsonElement task in values.EnumerateArray())
                {
                    var taskDto = new TaskDto
                    {
                        TaskId = task.GetProperty("taskId").GetInt32(),
                        Title = task.GetProperty("title").GetString(),
                        Description = task.GetProperty("description").GetString(),
                        DueDate = task.GetProperty("dueDate").GetDateTime(),
                        Status = task.GetProperty("status").GetString(),
                        EmployeeId = task.GetProperty("employeeId").GetInt32(),
                        Notes = task.GetProperty("notes").GetProperty("$values").EnumerateArray().Select(note => new CreateNoteDto
                        {
                            NoteId = note.GetProperty("noteId").GetInt32(),
                            Content = note.GetProperty("content").GetString(),
                            CreatedAt = note.GetProperty("createdAt").GetDateTime()
                        }).ToList(),
                        Documents = task.GetProperty("documents").GetProperty("$values").EnumerateArray().Select(document => new CreateEmployeeDocumentDto
                        {
                            DocumentId = document.GetProperty("documentId").GetInt32(),
                            FileName = document.GetProperty("fileName").GetString()
                        }).ToList()
                    };

                    tasks.Add(taskDto);
                }
            }

            return tasks;
        }
    }
}

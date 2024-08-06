using EmployeeTaskManagementSystem.Models.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Service
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<ResponseDto<List<CreateTaskDto>>> GetWeeklyReportAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/admin/reports/weekly");
                response.EnsureSuccessStatusCode();
                var rawContent = await response.Content.ReadAsStringAsync();

                var tasks = new List<CreateTaskDto>();
                using (JsonDocument doc = JsonDocument.Parse(rawContent))
                {
                    JsonElement root = doc.RootElement;
                    JsonElement data = root.GetProperty("data");
                    JsonElement values = data.GetProperty("$values");

                    foreach (JsonElement task in values.EnumerateArray())
                    {
                        var taskDto = new CreateTaskDto
                        {
                            TaskId = task.GetProperty("taskId").GetInt32(),
                            Title = task.GetProperty("title").GetString(),
                            Description = task.GetProperty("description").GetString(),
                            DueDate = task.GetProperty("dueDate").GetDateTime(),
                            Status = task.GetProperty("status").GetString(),
                            EmployeeId = task.GetProperty("employeeId").GetInt32()
                        };

                        tasks.Add(taskDto);
                    }
                }

                return new ResponseDto<List<CreateTaskDto>>
                {
                    StatusCode = 200, // Assuming success
                    Message = "Weekly report retrieved successfully",
                    Data = tasks
                };
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error fetching weekly report.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing weekly report response.", ex);
            }
        }

        public async Task<ResponseDto<List<CreateTaskDto>>> GetMonthlyReportAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/admin/reports/monthly");
                response.EnsureSuccessStatusCode();
                var rawContent = await response.Content.ReadAsStringAsync();

                var tasks = new List<CreateTaskDto>();
                using (JsonDocument doc = JsonDocument.Parse(rawContent))
                {
                    JsonElement root = doc.RootElement;
                    JsonElement data = root.GetProperty("data");
                    JsonElement values = data.GetProperty("$values");

                    foreach (JsonElement task in values.EnumerateArray())
                    {
                        var taskDto = new CreateTaskDto
                        {
                            TaskId = task.GetProperty("taskId").GetInt32(),
                            Title = task.GetProperty("title").GetString(),
                            Description = task.GetProperty("description").GetString(),
                            DueDate = task.GetProperty("dueDate").GetDateTime(),
                            Status = task.GetProperty("status").GetString(),
                            EmployeeId = task.GetProperty("employeeId").GetInt32()
                        };

                        tasks.Add(taskDto);
                    }
                }

                return new ResponseDto<List<CreateTaskDto>>
                {
                    StatusCode = 200, // Assuming success
                    Message = "Monthly report retrieved successfully",
                    Data = tasks
                };
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error fetching monthly report.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing monthly report response.", ex);
            }
        }


        // Helper class to match the nested structure
        public class RootData<T>
        {
            public T Values { get; set; }
        }

    }
}

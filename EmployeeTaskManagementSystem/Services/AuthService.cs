using EmployeeTaskManagementSystem.Models.Dto;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<LoginResponseDto> LoginAsync(UserLoginDto userLoginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", userLoginDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        }
    }

    public class LoginResponseDto
    {
        public string Token { get; set; }
        public int EmployeeId { get; set; }
        public string Role { get; set; }
    }
}

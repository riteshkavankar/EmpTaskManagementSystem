using EmployeeTaskManagementSystem.Models.Dto;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Service
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(UserLoginDto userLoginDto);
    }
}

using EmployeeTaskManagementSystem.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<IEnumerable<TaskDto>> GetTasksByEmployeeIdAsync(int employeeId);
    }
}

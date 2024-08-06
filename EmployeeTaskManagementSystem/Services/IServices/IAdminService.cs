using EmployeeTaskManagementSystem.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Service
{
    public interface IAdminService
    {
        Task<ResponseDto<List<CreateTaskDto>>> GetWeeklyReportAsync();
        Task<ResponseDto<List<CreateTaskDto>>> GetMonthlyReportAsync();
    }
}

using EmployeeTaskManagementSystem.Models;
using EmployeeTaskManagementSystem.Models.Dto;
using EmployeeTaskManagementSystem.Models.ViewModels;
using EmployeeTaskManagementSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Report()
        {
            try
            {
                var weeklyReport = await _adminService.GetWeeklyReportAsync();
                var monthlyReport = await _adminService.GetMonthlyReportAsync();

                var viewModel = new AdminReportViewModel
                {
                    WeeklyReport = weeklyReport?.Data ?? new List<CreateTaskDto>(),
                    MonthlyReport = monthlyReport?.Data ?? new List<CreateTaskDto>()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors as needed
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
            }
        }


    }
}

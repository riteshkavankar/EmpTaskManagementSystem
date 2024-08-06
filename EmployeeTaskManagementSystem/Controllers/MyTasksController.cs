using EmployeeTaskManagementSystem.Models.Dto;
using EmployeeTaskManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Controllers
{
    public class MyTasksController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;

        public MyTasksController(IEmployeeService employeeService, ITaskService taskService)
        {
            _employeeService = employeeService;
            _taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var employeeId = int.Parse(User.FindFirst("EmployeeId")?.Value ?? "0");
                if (employeeId == 0)
                {
                    return RedirectToAction("Index", "Home");
                }

                var tasks = await _employeeService.GetTasksByEmployeeIdAsync(employeeId);

                return View(tasks);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}

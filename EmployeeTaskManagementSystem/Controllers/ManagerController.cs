using EmployeeTaskManagementSystem.Models.Dto;
using EmployeeTaskManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;

        public ManagerController(IEmployeeService employeeService, ITaskService taskService)
        {
            _employeeService = employeeService;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllEmployeesTasks()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        public async Task<IActionResult> AssignTask()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            ViewBag.Employees = employees;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignTask(CreateTaskDto createTaskDto)
        {
            if (ModelState.IsValid)
            {
                await _taskService.CreateTaskAsync(createTaskDto);
                return RedirectToAction("AllEmployeesTasks");
            }

            var employees = await _employeeService.GetAllEmployeesAsync();
            ViewBag.Employees = employees;
            return View(createTaskDto);
        }
    }
}

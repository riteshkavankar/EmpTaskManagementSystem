using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using employeetaskapis.Data;
using employeetaskapis.Models;
using System.Threading.Tasks;
using employeetaskapis.Models.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;

namespace employeetaskapis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "Admin")]
        [Authorize(Policy = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var employee = new Employee
            {
                Name = createEmployeeDto.Name,
                Email = createEmployeeDto.Email
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var response = new ResponseDto<Employee>
            {
                StatusCode = 201,
                Message = "Employee created successfully",
                Data = employee
            };

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Tasks)
                .ThenInclude(t => t.Notes)
                .Include(e => e.Tasks)
                .ThenInclude(t => t.Documents)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound(new ResponseDto<string>
                {
                    StatusCode = 404,
                    Message = "Employee not found"
                });
            }

            var response = new ResponseDto<Employee>
            {
                StatusCode = 200,
                Message = "Employee retrieved successfully",
                Data = employee
            };

            return Ok(response);
        }

        [HttpGet("{id}/tasks")]
        public async Task<IActionResult> GetTasksByEmployeeId(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Tasks)
                .ThenInclude(t => t.Notes)
                .Include(e => e.Tasks)
                .ThenInclude(t => t.Documents)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound(new ResponseDto<string>
                {
                    StatusCode = 404,
                    Message = "Employee not found"
                });
            }

            var response = new ResponseDto<List<EmployeeTask>>
            {
                StatusCode = 200,
                Message = "Tasks retrieved successfully",
                Data = employee.Tasks
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();

            var employeeDtos = employees.Select(e => new CreateEmployeeDto
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Email = e.Email
            }).ToList();

            var response = new ResponseDto<List<CreateEmployeeDto>>
            {
                StatusCode = 200,
                Message = "Employees retrieved successfully",
                Data = employeeDtos
            };

            return Ok(response);
        }
    }
}

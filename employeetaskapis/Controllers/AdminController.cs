using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using employeetaskapis.Data;
using employeetaskapis.Models.Dto;
using employeetaskapis.Models;
using Microsoft.AspNetCore.Authorization;

namespace employeetaskapis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("reports/weekly")]
        public async Task<IActionResult> GetWeeklyReport()
        {
            var tasksDueInAWeek = await _context.Tasks
                .Where(t => t.DueDate >= DateTime.Now && t.DueDate <= DateTime.Now.AddDays(7))
                .ToListAsync();

            var response = new ResponseDto<List<EmployeeTask>>
            {
                StatusCode = 200,
                Message = "Weekly report retrieved successfully",
                Data = tasksDueInAWeek
            };

            return Ok(response);
        }

        [HttpGet("reports/monthly")]
        public async Task<IActionResult> GetMonthlyReport()
        {
            var tasksDueInAMonth = await _context.Tasks
                .Where(t => t.DueDate >= DateTime.Now && t.DueDate <= DateTime.Now.AddMonths(1))
                .ToListAsync();

            var response = new ResponseDto<List<EmployeeTask>>
            {
                StatusCode = 200,
                Message = "Monthly report retrieved successfully",
                Data = tasksDueInAMonth
            };

            return Ok(response);
        }
    }
}

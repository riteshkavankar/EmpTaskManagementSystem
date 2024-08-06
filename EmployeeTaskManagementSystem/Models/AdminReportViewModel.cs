using System;
using System.Collections.Generic;
using EmployeeTaskManagementSystem.Models.Dto;

namespace EmployeeTaskManagementSystem.Models.ViewModels
{
    public class AdminReportViewModel
    {
        public List<CreateTaskDto> WeeklyReport { get; set; } = new List<CreateTaskDto>();
        public List<CreateTaskDto> MonthlyReport { get; set; } = new List<CreateTaskDto>();
    }
}

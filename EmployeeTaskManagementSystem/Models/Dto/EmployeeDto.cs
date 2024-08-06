﻿namespace EmployeeTaskManagementSystem.Models.Dto
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<TaskDto> Tasks { get; set; }  
    }

}

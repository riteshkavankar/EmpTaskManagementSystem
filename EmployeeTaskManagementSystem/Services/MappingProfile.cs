using AutoMapper;
using EmployeeTaskManagementSystem.Models;
using EmployeeTaskManagementSystem.Models.Dto;

namespace EmployeeTaskManagementSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Employee
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CreateEmployeeDto, Employee>();

            // EmployeeTask
            CreateMap<EmployeeTask, CreateTaskDto>();
            CreateMap<CreateTaskDto, EmployeeTask>();

            // EmployeeDocument
            CreateMap<EmployeeDocument, CreateEmployeeDocumentDto>();
            CreateMap<CreateEmployeeDocumentDto, EmployeeDocument>();

            // Note
            CreateMap<Note, CreateNoteDto>();
            CreateMap<CreateNoteDto, Note>();

            // User
            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();
        }
    }
}

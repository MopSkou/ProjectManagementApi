using AutoMapper;
using ProjectManagementApi.Dto;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository;

namespace ProjectManagementApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Doljnosti, DoljnostiDto>();
            CreateMap<Companies, CompanyDto>();
            CreateMap<DoljnostiEmployee, DoljnostiEmployeeDto>();
            CreateMap<Employees, EmployeeDto>();
            CreateMap<ProjectEmployees, ProjectEmployeesDto>();
            CreateMap<Projects, ProjectDto>();
            CreateMap<Tasks, TaskDto>();

            CreateMap<DoljnostiDto, Doljnosti>();
            CreateMap<CompanyDto, Companies>();
            CreateMap<DoljnostiEmployeeDto, DoljnostiEmployee>();
            CreateMap<EmployeeDto, Employees>();
            CreateMap<ProjectEmployeesDto, ProjectEmployees>();
            CreateMap<ProjectDto, Projects>();
            CreateMap<TaskDto, Tasks>();
        }
    }
}
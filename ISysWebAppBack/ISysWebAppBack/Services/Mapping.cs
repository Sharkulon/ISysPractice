using AutoMapper;
using DomainLibBack.Organization;
using DomainLibBack.Units;
using DomainLibBack.Projects;
using ISysWebAppBack.ModelsDTO.OrganizationDTO;
using ISysWebAppBack.ModelsDTO.UnitsDTO;
using ISysWebAppBack.ModelsDTO.ProjectsDTO;
namespace ISysWebAppBack.Services

{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();

            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();

            CreateMap<Project, ProjectDTo>();
            CreateMap<ProjectDTo, Project>();
        }
    }
}

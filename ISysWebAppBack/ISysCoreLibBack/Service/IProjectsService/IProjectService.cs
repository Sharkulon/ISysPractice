using DomainLibBack.Projects;
using DomainLibBack.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISysCoreLibBack.Service.IProjectsService
{
    public interface IProjectService
    {
        ICollection<Project>? GetProjects();
        Task<ICollection<Project>>? GetProjectsAsync();

        Project? GetProjectByCode(string projectCode);
        Task<Project>? GetProjectByCodeAsync(string projectCode);

        bool ProjectExists(string projectCode);
        Task<bool> ProjectExistsAsync(string projectCode);

        bool ParticipatesInProject(string projectCode, int codeEmployee);
        Task<bool> ParticipatesInProjectAsync(string projectCode, int codeEmployee);

        ICollection<Employee>? GetAllEmployeesInProject(string projectCode);
        Task<ICollection<Employee>> GetAllEmployeesInProjectAsync(string projectCode);

        bool CreateProject(Project project);
        bool UpdateProject(Project project);
        bool DeleteProject(string projectCode);
        bool Save();

        Task<bool> CreateProjectAsync(Project project);
        Task<bool> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(string projectCode);
        Task<bool> SaveAsync();
    }
}

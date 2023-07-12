using DomainLibBack.Projects;
using DomainLibBack.Units;

namespace ISysCoreLibBack.Service.IUtilsService
{
    public interface IEmployeeProjectService
    {
        bool ParticipatesInProject(string projectCode, int codeEmployee);
        Task<bool> ParticipatesInProjectAsync(string projectCode, int codeEmployee);
 

        ICollection<Employee>? GetAllEmployeesInProject(string projectCode);
        Task<ICollection<Employee>> GetAllEmployeesInProjectAsync(string projectCode);
    

        ICollection<Project>? GetAllProjectsEmployee(int employeeCode);
        Task<ICollection<Project>>? GetAllProjectsEmployeeAsync(int employeeCode);


        bool AddEmployeeInProject(string projectCode, int employeeCode);
        bool AddEmployeeInProject(Project project, Employee employee);

        Task<bool> AddEmployeeInProjectAsync(string projectCode, int employeeCode);
        Task<bool> AddEmployeeInProjectAsync(Project project, Employee employee);


        bool RemoveEmployeeFromProject(string projectCode, int employeeCode);
        bool RemoveEmployeeFromProject(Project project, Employee employee);

        Task<bool> RemoveEmployeeFromProjectAsync(string projectCode, int employeeCode);
        Task<bool> RemoveEmployeeFromProjectAsync(Project project, Employee employee);


        bool Save();
        Task<bool> SaveAsync();


        bool RemoveEmployeeFromAllProject(int employeeCode);
        Task<bool> RemoveEmployeeFromAllProjectAsync(int employeeCode);


        bool RemoveProjectFromAllEmployees(string projectCode);
        Task<bool> RemoveProjectFromAllEmployeesAsync(string projectCode);
    }
}

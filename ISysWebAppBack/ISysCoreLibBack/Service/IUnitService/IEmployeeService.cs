using DomainLibBack.Organization;
using DomainLibBack.Projects;
using DomainLibBack.Units;

namespace ISysCoreLibBack.Service.IUnitService
{
    public interface IEmployeeService
    {
        Department GetIdDepartment(string departmentCode);
        ICollection<Employee>? GetEmployees();
        Task<ICollection<Employee>>? GetEmployeesAsync();

        Employee? GetEmployeeByCode(int employeeCode);
        Task<Employee>? GetEmployeeByCodeAsync(int employeeCode);

        ICollection<Project>? GetProject(int employeeCode);
        Task<ICollection<Project>>? GetProjectsAsync(int employeeCode);

        bool EmployeeExists(int employeeCode);
        Task<bool> EmployeeExistsAsync(int employeeCode);

        bool ParticipatesInProject(string projectCode, int employeeCode);
        Task<bool> ParticipatesInProjectAsync(string projectCode, int employeeCode);

        ICollection<Employee>? GetSubEmployees(int employeeCode);
        Task<ICollection<Employee>>? GetSubEmployeesAsync(int employeeCode);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(int employeeCode);
        bool Save();

        Task<bool> CreateEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int employeeCode);
        Task<bool> SaveAsync();

    }
}

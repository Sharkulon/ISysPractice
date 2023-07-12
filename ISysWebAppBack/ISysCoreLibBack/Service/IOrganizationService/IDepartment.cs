using DomainLibBack.Organization;
using DomainLibBack.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISysCoreLibBack.Service.IOrganizationService
{
    public interface IDepartment
    {
        ICollection<Department>? GetDepartments();
        Task<ICollection<Department>>? GetDepartmentsAsync();

        Department? GetDepartmentByCode(string subdivisionCode);
        Task<Department>? GetDepartmentByCodeAsync(string subdivisionCode);

        bool DepartmentExists(string subdivisionCode);
        Task<bool> DepartmentExistsAsync(string subdivisionCode);

        ICollection<Employee>? GetEmployees(string subdivisionCode);
        Task<ICollection<Employee>>? GetEmployeesAsync(string subdivisionCode);
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(string subdivisionCode);
        bool Save();

        Task<bool> CreateDepartmentAsync(Department department);
        Task<bool> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(string subdivisionCode);
        Task<bool> SaveAsync();
    }
}

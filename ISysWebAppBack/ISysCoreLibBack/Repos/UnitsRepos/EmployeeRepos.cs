using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ISysCoreLibBack.Valid.UnitsValid;
using ISysCoreLibBack.Repos.UtilsRepos;
using ISysDataBaseBack.DBContext;
using DomainLibBack.Units;
using DomainLibBack.Projects;
using ISysCoreLibBack.Service.IUnitService;
using DomainLibBack.Organization;

namespace ISysCoreLibBack.Repos.UnitsRepos
{
    public class EmployeeRepos : IEmployeeService
    {
        private readonly DataBaseContext _dbContext;
        private readonly EmployeeValid _validator;
        public EmployeeRepos(DataBaseContext dbContext,
            EmployeeValid validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }
        public Department GetIdDepartment(string departmentCode) =>
            _dbContext.Departments
            .First(x => x.SubdivisionCode == departmentCode);
        public bool EmployeeExists(int employeeCode) =>
            _dbContext.Employees
            .FirstOrDefault(x => x.EmployeeCode == employeeCode) == null ?
            false : true;
        public async Task<bool> EmployeeExistsAsync(int employeeCode) =>
            await _dbContext.Employees
            .FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode) == null ?
            false : true;

        public Employee? GetEmployeeByCode(int employeeCode) =>
            _dbContext.Employees
            .FirstOrDefault(x => x.EmployeeCode == employeeCode);
        public async Task<Employee>? GetEmployeeByCodeAsync(int employeeCode) =>
            await _dbContext.Employees
            .FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode);

        public ICollection<Employee>? GetEmployees() =>
            _dbContext.Employees.ToList();
        public async Task<ICollection<Employee>>? GetEmployeesAsync() =>
            await _dbContext.Employees.ToListAsync();

        public ICollection<Project>? GetProject(int employeeCode) =>
            _dbContext.EmployeeProjects
            .Where(x => x.Employee.EmployeeCode == employeeCode)
            .Select(x => x.Project)
            .ToList();
        public async Task<ICollection<Project>>? GetProjectsAsync(int employeeCode) =>
            await _dbContext.EmployeeProjects
            .Where(x => x.Employee.EmployeeCode == employeeCode)
            .Select(x => x.Project)
            .ToListAsync();

        public ICollection<Employee>? GetSubEmployees(int employeeCode) =>
            _dbContext.Employees
            .Where(x => x.HeadManager!.EmployeeCode == employeeCode)
            .ToList();
        public async Task<ICollection<Employee>>? GetSubEmployeesAsync(int employeeCode) =>
            await _dbContext.Employees
            .Where(x => x.HeadManager!.EmployeeCode == employeeCode)
            .ToListAsync();

        public bool ParticipatesInProject(string projectCode, int employeeCode) =>
            new EmployeeProjectRepos(_dbContext)
            .ParticipatesInProject(projectCode, employeeCode);

        public async Task<bool> ParticipatesInProjectAsync(string projectCode,
            int employeeCode) =>
            await new EmployeeProjectRepos(_dbContext)
            .ParticipatesInProjectAsync(projectCode, employeeCode);

        public bool Save() =>
            _dbContext.SaveChanges() > 0 ? true : false;

        public async Task<bool> SaveAsync() =>
            await _dbContext.SaveChangesAsync() > 0 ? true : false;

        public bool CreateEmployee(Employee employee)
        {
            _validator.ValidateAndThrow(employee);
            _dbContext.Employees.Add(employee);
            return Save();
        }

        public async Task<bool> CreateEmployeeAsync(Employee employee)
        {
            await _validator.ValidateAndThrowAsync(employee);
            await _dbContext.Employees.AddAsync(employee);
            return await SaveAsync();
        }

        public bool DeleteEmployee(int employeeCode)
        {
            var employee = _dbContext.Employees
                .FirstOrDefault(x => x.EmployeeCode == employeeCode);
            if (employee == null)
                return false;
            _dbContext.Employees.Remove(employee);

            return Save();
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeCode)
        {
            var employee = await _dbContext.Employees
                .FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode);
            if (employee == null)
                return false;
            _dbContext.Employees.Remove(employee);
            return await SaveAsync();
        }

        public bool UpdateEmployee(Employee employee)
        {
            _validator.ValidateAndThrow(employee);

            _dbContext.Employees.Update(employee);
            return Save();
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            await _validator.ValidateAndThrowAsync(employee);

            _dbContext.Employees.Update(employee);
            return await SaveAsync();
        }
    }
}

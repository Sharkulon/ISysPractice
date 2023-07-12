using FluentValidation;
using ISysDataBaseBack.DBContext;
using ISysCoreLibBack.Valid.OrganizationValid;
using ISysCoreLibBack.Service.IOrganizationService;
using DomainLibBack.Organization;
using DomainLibBack.Units;
using Microsoft.EntityFrameworkCore;


namespace ISysCoreLibBack.Repos.OrganizationRepos
{
    public class DepartmentRepos : IDepartment
    {
        private readonly DataBaseContext _dbContext;
        private readonly DepartmentValid _validator;
        public DepartmentRepos(DataBaseContext dbContext,
            DepartmentValid validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public bool DepartmentExists(string subdivisionCode) =>
            _dbContext.Departments
            .FirstOrDefault(x => x.SubdivisionCode == subdivisionCode) == null ?
            false : true;

        public async Task<bool> DepartmentExistsAsync(string subdivisionCode) =>
            await _dbContext.Departments
            .FirstOrDefaultAsync(x => x.SubdivisionCode == subdivisionCode) == null ?
            false : true;

        public Department? GetDepartmentByCode(string subdivisionCode) =>
            _dbContext.Departments
            .FirstOrDefault(x => x.SubdivisionCode == subdivisionCode);

        public async Task<Department>? GetDepartmentByCodeAsync(string subdivisionCode) =>
            await _dbContext.Departments
            .FirstOrDefaultAsync(x => x.SubdivisionCode == subdivisionCode);

        public ICollection<Department>? GetDepartments() =>
            _dbContext.Departments.ToList();

        public async Task<ICollection<Department>>? GetDepartmentsAsync() =>
            await _dbContext.Departments.ToListAsync();

        public ICollection<Employee>? GetEmployees(string subdivisionCode) =>
            _dbContext.Employees
            .Where(x => x.Department.SubdivisionCode == subdivisionCode)
            .ToList();

        public async Task<ICollection<Employee>>? GetEmployeesAsync(string subdivisionCode) =>
            await _dbContext.Employees
            .Where(x => x.Department.SubdivisionCode == subdivisionCode)
            .ToListAsync();

        public bool Save() =>
            _dbContext.SaveChanges() > 0 ? true : false;

        public async Task<bool> SaveAsync() =>
            await _dbContext.SaveChangesAsync() > 0 ? true : false;

        public bool CreateDepartment(Department department)
        {
            _validator.ValidateAndThrow(department);
            _dbContext.Departments.Add(department);
            return Save();
        }

        public async Task<bool> CreateDepartmentAsync(Department department)
        {
            await _validator.ValidateAndThrowAsync(department);
            await _dbContext.Departments.AddAsync(department);
            return await SaveAsync();
        }

        public bool DeleteDepartment(string subdivisionCode)
        {
            var department = _dbContext.Departments
                .FirstOrDefault(x => x.SubdivisionCode == subdivisionCode);
            if (department == null)
                return false;
            _dbContext.Departments.Remove(department);
            return Save();
        }

        public async Task<bool> DeleteDepartmentAsync(string subdivisionCode)
        {
            var department = await _dbContext.Departments
                .FirstOrDefaultAsync(x => x.SubdivisionCode == subdivisionCode);
            if (department == null)
                return false;
            _dbContext.Departments.Remove(department);
            return await SaveAsync();
        }
        public bool UpdateDepartment(Department department)
        {
            _validator.ValidateAndThrow(department);
            _dbContext.Departments.Update(department);
            return Save();
        }

        public async Task<bool> UpdateDepartmentAsync(Department department)
        {
            await _validator.ValidateAndThrowAsync(department);
            _dbContext.Departments.Update(department);
            return await SaveAsync();
        }
    }
}

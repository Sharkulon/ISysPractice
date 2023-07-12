using DomainLibBack.Projects;
using DomainLibBack.Units;
using DomainLibBack.Utils;
using ISysCoreLibBack.Service.IUtilsService;
using ISysDataBaseBack.DBContext;
using Microsoft.EntityFrameworkCore;

namespace ISysCoreLibBack.Repos.UtilsRepos
{
    public class EmployeeProjectRepos : IEmployeeProjectService
    {
        private readonly DataBaseContext _dbContext;
        public EmployeeProjectRepos(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddEmployeeInProject(string projectCode, int employeeCode)
        {
            var project = _dbContext.Projects
                .FirstOrDefault(x => x.ProjectCode == projectCode);
            if (project == null)
            {
                return false;
            }

            var employee = _dbContext.Employees
                .FirstOrDefault(x => x.EmployeeCode == employeeCode);
            if (employee == null)
            {
                return false;
            }

            _dbContext.EmployeeProjects.Add(new EmployeeProject
            {
                Employee = employee,
                IdEmployee = employee.Id,
                Project = project,
                IdProject = project.Id
            });

            return Save();
        }

        public bool AddEmployeeInProject(Project project, Employee employee)
        {
            if (_dbContext.Projects.Find(project) == null)
            {
                return false;
            }
            if (_dbContext.Employees.Find(employee) == null)
            {
                return false;
            }

            _dbContext.EmployeeProjects.Add(new EmployeeProject
            {
                Employee = employee,
                IdEmployee = employee.Id,
                Project = project,
                IdProject = project.Id
            });

            return Save();
        }

        public async Task<bool> AddEmployeeInProjectAsync(string projectCode,
            int employeeCode)
        {
            var project = await _dbContext.Projects
                .FirstOrDefaultAsync(x => x.ProjectCode == projectCode);
            if (project == null)
            {
                return false;
            }

            var employee = await _dbContext.Employees
                .FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode);
            if (employee == null)
            {
                return false;
            }

            _dbContext.EmployeeProjects.Add(new EmployeeProject
            {
                Employee = employee,
                IdEmployee = employee.Id,
                Project = project,
                IdProject = project.Id
            });

            return await SaveAsync();
        }

        public async Task<bool> AddEmployeeInProjectAsync(Project project,
            Employee employee)
        {
            if (await _dbContext.Projects.FindAsync(project) == null)
            {
                return false;
            }
            if (await _dbContext.Employees.FindAsync(employee) == null)
            {
                return false;
            }

            _dbContext.EmployeeProjects.Add(new EmployeeProject
            {
                Employee = employee,
                IdEmployee = employee.Id,
                Project = project,
                IdProject = project.Id
            });

            return await SaveAsync();
        }

        public ICollection<Employee>? GetAllEmployeesInProject(string projectCode) =>
            _dbContext.EmployeeProjects
            .Where(x => x.Project.ProjectCode == projectCode)
            .Select(x => x.Employee)
            .ToList();

        public async Task<ICollection<Employee>> GetAllEmployeesInProjectAsync
            (string projectCode) =>
            await _dbContext.EmployeeProjects
            .Where(x => x.Project.ProjectCode == projectCode)
            .Select(x => x.Employee)
            .ToListAsync();

        public ICollection<Project>? GetAllProjectsEmployee(int employeeCode) =>
            _dbContext.EmployeeProjects
            .Where(x => x.Employee.EmployeeCode == employeeCode)
            .Select(x => x.Project)
            .ToList();

        public async Task<ICollection<Project>>? GetAllProjectsEmployeeAsync
            (int employeeCode) =>
            await _dbContext.EmployeeProjects
            .Where(x => x.Employee.EmployeeCode == employeeCode)
            .Select(x => x.Project)
            .ToListAsync();

        public bool ParticipatesInProject(string projectCode, int codeEmployee) =>
            _dbContext.EmployeeProjects
            .FirstOrDefault(x => x.Employee!.EmployeeCode == codeEmployee &&
            x.Project!.ProjectCode == projectCode) == null ? false : true;

        public async Task<bool> ParticipatesInProjectAsync(string projectCode, int codeEmployee) =>
            await _dbContext.EmployeeProjects
            .FirstOrDefaultAsync(x => x.Employee!.EmployeeCode == codeEmployee &&
            x.Project!.ProjectCode == projectCode) == null ? false : true;

        public bool RemoveEmployeeFromProject(string projectCode, int employeeCode)
        {
            var employeeProject = _dbContext.EmployeeProjects
                .FirstOrDefault(x => x.Project!.ProjectCode == projectCode &&
                x.Employee!.EmployeeCode == employeeCode);

            if (employeeProject == null)
            {
                return false;
            }
            _dbContext.EmployeeProjects.Remove(employeeProject);
            return Save();
        }

        public bool RemoveEmployeeFromProject(Project project, Employee employee)
        {
            var employeeProject = _dbContext.EmployeeProjects
                .FirstOrDefault(x => x.Project!.Equals(project) &&
                x.Employee!.Equals(employee));

            if (employeeProject == null)
            {
                return false;
            }
            _dbContext.EmployeeProjects.Remove(employeeProject);
            return Save();
        }

        public async Task<bool> RemoveEmployeeFromProjectAsync(string projectCode, int employeeCode)
        {
            var employeeProject = await _dbContext.EmployeeProjects
                .FirstOrDefaultAsync(x => x.Project!.ProjectCode == projectCode &&
                x.Employee!.EmployeeCode == employeeCode);

            if (employeeProject == null)
            {
                return false;
            }
            _dbContext.EmployeeProjects.Remove(employeeProject);
            return await SaveAsync();
        }

        public async Task<bool> RemoveEmployeeFromProjectAsync(Project project, Employee employee)
        {
            var employeeProject = await _dbContext.EmployeeProjects
              .FirstOrDefaultAsync(x => x.Project!.Equals(project) &&
              x.Employee!.Equals(employee));

            if (employeeProject == null)
            {
                return false;
            }
            _dbContext.EmployeeProjects.Remove(employeeProject);
            return await SaveAsync();
        }

        public bool Save() =>
            _dbContext.SaveChanges() > 0 ? true : false;
        public async Task<bool> SaveAsync() =>
            await _dbContext.SaveChangesAsync() > 0 ? true : false;


        public bool RemoveEmployeeFromAllProject(int employeeCode)
        {
            _dbContext.EmployeeProjects
                .RemoveRange(_dbContext.EmployeeProjects
                    .Where(x => x.Employee!.EmployeeCode == employeeCode)
                    .ToList());

            return Save();
        }
        public async Task<bool> RemoveEmployeeFromAllProjectAsync(int employeeCode)
        {
            _dbContext.EmployeeProjects
                .RemoveRange(_dbContext.EmployeeProjects
                    .Where(x => x.Employee!.EmployeeCode == employeeCode)
                    .ToList());

            return await SaveAsync();
        }

        public bool RemoveProjectFromAllEmployees(string projectCode)
        {
            _dbContext.EmployeeProjects
                .RemoveRange(_dbContext.EmployeeProjects
                .Where(x => x.Project!.ProjectCode == projectCode)
                .ToList());

            return Save();
        }
        public async Task<bool> RemoveProjectFromAllEmployeesAsync(string projectCode)
        {
            _dbContext.EmployeeProjects
                .RemoveRange(_dbContext.EmployeeProjects
                .Where(x => x.Project!.ProjectCode == projectCode)
                .ToList());
            return await SaveAsync();
        }
    }
}

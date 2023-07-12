using DomainLibBack.Organization;
using DomainLibBack.Projects;
using DomainLibBack.Units;
using DomainLibBack.Utils;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ISysDataBaseBack.DBContext
{
    /// <summary>
    /// Parent class for db context
    /// </summary>
    public class DataBaseContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        public DataBaseContext() : base() { }
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options) { Database.EnsureCreated(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

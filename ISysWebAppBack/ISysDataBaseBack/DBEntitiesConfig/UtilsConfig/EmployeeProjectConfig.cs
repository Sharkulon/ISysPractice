using DomainLibBack.Projects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DomainLibBack.Utils;

namespace ISysDataBaseBack.DBEntitiesConfig.UtilsConfig
{
    /// <summary>
    /// Configuration for EmployeeProject
    /// </summary>
    public class EmployeeProjectConfig : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder
                .HasKey(x => x.Key);

            builder
                .HasOne(x => x.Employee)
                .WithMany(x => x.EmployeeProjects)
                .HasForeignKey(x => x.IdEmployee);
            builder
                .HasOne(x => x.Project)
                .WithMany(x => x.EmployeeProject)
                .HasForeignKey(x => x.IdProject);
        }
    }
}

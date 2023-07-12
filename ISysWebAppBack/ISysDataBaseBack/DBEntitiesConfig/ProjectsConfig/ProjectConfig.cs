using DomainLibBack.Projects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISysDataBaseBack.DBEntitiesConfig.ProjectsConfig
{
    /// <summary>
    /// Configuration for Project
    /// </summary>
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .IsRequired();
            builder
                .Property(x => x.Title)
                .HasMaxLength(63);
            builder
                .Property(x => x.ProjectCode)
                .HasMaxLength(63);
        }
    }
}

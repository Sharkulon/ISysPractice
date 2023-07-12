﻿using DomainLibBack.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISysDataBaseBack.DBEntitiesConfig.OrganizationConfig
{
    /// <summary>
    /// Configuration for Department
    /// </summary>
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .IsRequired();
            builder
                .HasMany(x => x.Employees)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.IdDepartment);
            builder
                .Property(x => x.SubdivisionCode)
                .HasMaxLength(63);
            builder
                .Property(x => x.Title)
                .HasMaxLength(63);
        }
    }
}

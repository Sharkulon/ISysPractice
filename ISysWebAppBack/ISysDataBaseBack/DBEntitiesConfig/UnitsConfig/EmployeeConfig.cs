using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibBack.Units;

namespace ISysDataBaseBack.DBEntitiesConfig.UnitsConfig
{
 /// <summary>
 /// Configuration for Employee
 /// </summary>
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .IsRequired();
            builder
                .Property(x => x.Name)
                .HasMaxLength(63);
            builder
                .Property(x => x.Surname)
                .HasMaxLength(63);
            builder
                .Property(x => x.Patronymic)
                .HasMaxLength(63);
            builder
                .Property(x => x.Phone)
                .HasMaxLength(15);
            builder
                .Property(x => x.Email)
                .HasMaxLength(63);
            builder
                .Property(x => x.JobTitle)
                .HasMaxLength(63);
            builder
                .HasIndex(x => x.EmployeeCode)
                .IsUnique(true);
        }
    }
}

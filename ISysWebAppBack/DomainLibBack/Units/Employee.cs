﻿using DomainLibBack.Organization;
using DomainLibBack.Utils;
using System.ComponentModel.DataAnnotations;
namespace DomainLibBack.Units
{
    public enum Status
    {
        /// <summary>
        /// Actively working
        /// </summary>
        Active,
        /// <summary>
        /// On vacation
        /// </summary>
        Vacation,
        /// <summary>
        /// Employee is fired
        /// </summary>
        Fired,
        /// <summary>
        /// An employee is sick.
        /// </summary>
        SickLeave,
        /// <summary>
        /// The employee is on maternity leave
        /// </summary>
        MaternityLeave
    }
    public class Employee
    {
        /// <summary>
        /// Autogenerated primary key in string format. 
        /// Represents Guid
        /// </summary>
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name of the person
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Surname of the person
        /// </summary>
        public string? Surname { get; set; }
        /// <summary>
        /// Patronymic of the person
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Birthday. Format DD.MM.YYYY
        /// </summary>
        public DateOnly? BirthDay { get; set; }

        /// <summary>
        /// Email. Format lalala@lala.la
        /// </summary>
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public string? Email { get; set; }

        /// <summary>
        /// Phone. Format @[0-9]{11}
        /// </summary>
        [RegularExpression(@"[0-9]{10}")]
        public string? Phone { get; set; }
        /// <summary>
        /// Job title
        /// </summary>
        public string JobTitle { get; set; } = String.Empty;

        /// <summary>
        /// Internal employee code. It is unique. 
        /// It is required to identify a specific employee.
        /// The default value is -1
        /// </summary>
        public int EmployeeCode { get; set; } = -1;

        /// <summary>
        /// Id of the employee's supervisor
        /// </summary>
        public string? IdHeadManager { get; set; }
        /// <summary>
        /// Link of the employee's supervisor
        /// </summary>
        public Employee? HeadManager { get; set; }

        /// <summary>
        /// Id of the department in which the person works
        /// </summary>
        public string IdDepartment { get; set; } = String.Empty;
        /// <summary>
        /// Link of the department in which the person works
        /// </summary>
        public Department Department { get; set; } = new();

        /// <summary>
        /// Employee's job status
        /// </summary>
        public Status Status { get; set; } = Status.Active;

        /// <summary>
        /// A field that implements a many-to-many relationship 
        /// with the Project class
        /// </summary>
        public ICollection<EmployeeProject>? EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
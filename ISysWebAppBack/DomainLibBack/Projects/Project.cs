﻿using System.ComponentModel.DataAnnotations;
using DomainLibBack.Utils;

namespace DomainLibBack.Projects
{
    public class Project
    {
        /// <summary>
        /// Autogenerated primary key in string format. Represents Guid
        /// </summary>
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name of department
        /// </summary>
        public string Title { get; set; } = String.Empty;

        /// <summary>
        /// Project code
        /// </summary>
        public string ProjectCode { get; set; } = String.Empty;

        /// <summary>
        /// A field that implements a many-to-many relationship with the Employee class
        /// </summary>
        public ICollection<EmployeeProject>? EmployeeProject { get; set; } = new List<EmployeeProject>();
    }
}

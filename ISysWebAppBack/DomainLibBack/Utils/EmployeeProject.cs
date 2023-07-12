using DomainLibBack.Projects;
using DomainLibBack.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibBack.Utils
{
    public class EmployeeProject
    {
        /// <summary>
        /// Id of Employee
        /// </summary>
        public string IdEmployee { get; set; } = String.Empty;
        /// <summary>
        /// Link of Employee
        /// </summary>
        public Employee? Employee { get; set; }

        /// <summary>
        /// Id of Project
        /// </summary>
        public string IdProject { get; set; } = String.Empty;
        /// <summary>
        /// Link of Project
        /// </summary>
        public Project? Project { get; set; }

        /// <summary>
        /// The key for the supporting table, 
        /// because without it it does not work for me :(
        /// </summary>
        public string Key { get; set; } = new Guid().ToString();
    }
}

using DomainLibBack.Projects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISysCoreLibBack.Valid.ProjectsValid
{
    /// <summary>
    /// Validation for Project
    /// </summary>
    public class ProjectValid : AbstractValidator<Project>
    {
        public ProjectValid()
        {
            RuleFor(x => x.ProjectCode).NotEmpty()
                .WithMessage("The ProjectCode field cannot be empty");
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("The Title field cannot be empty");
        }
    }
}

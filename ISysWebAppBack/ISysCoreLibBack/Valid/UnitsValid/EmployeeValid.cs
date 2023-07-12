using DomainLibBack.Units;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISysCoreLibBack.Valid.UnitsValid
{
    public class EmployeeValid : AbstractValidator<Employee>
    {
        public EmployeeValid()
        {
            RuleFor(x => x.JobTitle).NotEmpty()
                .WithMessage("The CodeEmployee field cannot be empty");
        }
    }
}

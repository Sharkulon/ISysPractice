using DomainLibBack.Organization;
using FluentValidation;

namespace ISysCoreLibBack.Valid.OrganizationValid
{
    /// <summary>
    /// Validation for Department
    /// </summary>
    public class DepartmentValid : AbstractValidator<Department>
    {
        public DepartmentValid()
        {
            RuleFor(x => x.SubdivisionCode).NotEmpty()
                .WithMessage("The SubdivisionCode field cannot be empty");
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("The Title field cannot be empty");
        }
    }
}

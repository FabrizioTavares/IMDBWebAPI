using FluentValidation;
using Service.Abstraction.AdministrativeServiceAbstractions.AdminDTOs;

namespace Service.Abstraction.AdministrativeServiceAbstractions.AdminValidation;

public class CreateAdminDTOValidator : AbstractValidator<CreateAdminDTO>
{
    public CreateAdminDTOValidator()
    {
        RuleFor(a => a.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(a => a.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(a => a.Password).MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        RuleFor(a => a.Hierarchy).GreaterThan(-1).WithMessage("Hierarchy must be greater than -1");
    }
}
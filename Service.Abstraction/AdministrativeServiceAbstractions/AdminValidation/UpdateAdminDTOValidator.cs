using FluentValidation;
using Service.Abstraction.AdministrativeServiceAbstractions.AdminDTOs;

namespace Service.Abstraction.AdministrativeServiceAbstractions.AdminValidation;

public class UpdateAdminDTOValidator : AbstractValidator<UpdateAdminDTO>
{
    public UpdateAdminDTOValidator()
    {
        RuleFor(a => a.NewPassword).MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long")
            .When(a => a.NewPassword != null);
    }
}
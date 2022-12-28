using Domain.DTOs.AdminDTOs;
using FluentValidation;

namespace Service.Validation.Admin
{
    public class UpdateAdminDTOValidator : AbstractValidator<UpdateAdminDTO>
    {
        public UpdateAdminDTOValidator()
        {
            RuleFor(a => a.NewPassword).MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long")
                .When(a => a.NewPassword != null);
        }
    }
}

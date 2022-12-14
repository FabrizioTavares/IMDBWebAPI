using FluentValidation;
using Service.Abstraction.UserServiceAbstractions.UserDTOs;

namespace Service.Abstraction.UserServiceAbstractions.UserValidation;

public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserDTOValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("User name is required").When(x => x.Username != null);
        RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must be at least 8 characters long").When(x => x.Password != null);
    }
}
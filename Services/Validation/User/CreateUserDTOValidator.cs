using Domain.DTOs.UserDTOs;
using FluentValidation;

namespace Service.Validation.User;

public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
{
    public CreateUserDTOValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("User name is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must be at least 8 characters long");
    }
}
using Domain.DTOs.UserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validation.User
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            // TODO: Check rules: not all properties will be changed.
            RuleFor(x => x.Username).NotEmpty().WithMessage("User name is required").When(x => x.Username != null);
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must be at least 8 characters long").When(x => x.Password != null);
        }
    }
}

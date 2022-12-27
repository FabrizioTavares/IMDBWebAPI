using Domain.DTOs.AdminDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validation.Admin
{
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
}

using Domain.DTOs.AdminDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

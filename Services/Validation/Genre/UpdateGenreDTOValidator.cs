using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validation.Genre
{
    public class UpdateGenreDTOValidator : AbstractValidator<Domain.DTOs.GenreDTOs.UpdateGenreDTO>
    {
        public UpdateGenreDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(50)
                .WithMessage("Title must be less than 50 characters");
        }
    }
}

using Domain.DTOs.GenreDTOs;
using FluentValidation;

namespace Service.Validation.Genre;

public class UpdateGenreDTOValidator : AbstractValidator<UpdateGenreDTO>
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
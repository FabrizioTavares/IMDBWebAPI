using FluentValidation;
using Service.Abstraction.GenreServiceAbstractions.GenreDTOs;

namespace Service.Abstraction.GenreServiceAbstractions.GenreValidation;

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
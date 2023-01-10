using FluentValidation;
using Service.Abstraction.GenreServiceAbstractions.GenreDTOs;

namespace Service.Abstraction.GenreServiceAbstractions.GenreValidation;

public class CreateGenreDTOValidator : AbstractValidator<CreateGenreDTO>
{
    public CreateGenreDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(50)
            .WithMessage("Title must be less than 50 characters");
    }
}
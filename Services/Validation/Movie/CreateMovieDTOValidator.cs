using Domain.DTOs.MovieDTOs;
using FluentValidation;

namespace Service.Validation.Movie
{
    public class CreateMovieDTOValidator : AbstractValidator<CreateMovieDTO>
    {
        public CreateMovieDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("A movie's title is required");
            RuleFor(x => x.Title).MaximumLength(100).WithMessage("A movie's title must be less than 100 characters");
            RuleFor(x => x.ReleaseYear).GreaterThanOrEqualTo(1888).WithMessage("Release year must be equal or greater than 1888.");
            RuleFor(x => x.Duration).GreaterThan(0).WithMessage("A movie must have its duration greater than 0.");
        }
    }
}

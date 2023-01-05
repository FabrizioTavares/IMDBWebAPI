using Domain.DTOs.MovieDTOs;
using FluentValidation;

namespace Service.Validation.Movie
{
    public class UpdateMovieDTOValidator : AbstractValidator<UpdateMovieDTO>
    {
        public UpdateMovieDTOValidator()
        {
            RuleFor(x => x.Title).MaximumLength(100).WithMessage("Title must be less than 100 characters");
        }
    }
}
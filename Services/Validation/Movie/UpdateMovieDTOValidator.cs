using Domain.DTOs.MovieDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

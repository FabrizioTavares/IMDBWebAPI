using Domain.DTOs.PerformanceDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validation.Performance
{
    public class CreatePerformanceDTOValidator : AbstractValidator<CreatePerformanceDTO>
    {
        public CreatePerformanceDTOValidator()
        {
            RuleFor(x => x.MovieId).NotEmpty().WithMessage("MovieId is required");
            RuleFor(x => x.ParticipantId).NotEmpty().WithMessage("ParticipantId is required");
            RuleFor(x => x.CharacterName).MaximumLength(100).WithMessage("Character name must be less than 100 characters");
        }
    }
}

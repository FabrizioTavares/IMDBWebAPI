using Domain.DTOs.PerformanceDTOs;
using FluentValidation;

namespace Service.Validation.Performance
{
    public class UpdatePerformanceDTOValidator : AbstractValidator<UpdatePerformanceDTO>
    {
        public UpdatePerformanceDTOValidator()
        {
            RuleFor(x => x.CharacterName).MaximumLength(100).WithMessage("Character name must be less than 100 characters");
        }
    }
}
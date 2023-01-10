using FluentValidation;
using Service.Abstraction.MovieServiceAbstractions.PerformanceDTOs;

namespace Service.Abstraction.MovieServiceAbstractions.PerformanceValidation;

public class UpdatePerformanceDTOValidator : AbstractValidator<UpdatePerformanceDTO>
{
    public UpdatePerformanceDTOValidator()
    {
        RuleFor(x => x.CharacterName).MaximumLength(100).WithMessage("Character name must be less than 100 characters");
    }
}
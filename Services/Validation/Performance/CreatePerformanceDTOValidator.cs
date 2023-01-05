using Domain.DTOs.PerformanceDTOs;
using FluentValidation;

namespace Service.Validation.Performance;

public class CreatePerformanceDTOValidator : AbstractValidator<CreatePerformanceDTO>
{
    public CreatePerformanceDTOValidator()
    {
        RuleFor(x => x.ParticipantId).NotEmpty().WithMessage("ParticipantId is required");
        RuleFor(x => x.CharacterName).NotEmpty().WithMessage("Character name is required");
        RuleFor(x => x.CharacterName).MaximumLength(100).WithMessage("Character name must be less than 100 characters");
    }
}
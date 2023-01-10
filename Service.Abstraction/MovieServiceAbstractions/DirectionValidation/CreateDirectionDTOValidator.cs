using FluentValidation;
using Service.Abstraction.MovieServiceAbstractions.DirectionDTOs;

namespace Service.Abstraction.MovieServiceAbstractions.DirectionValidation;

public class CreateDirectionDTOValidator : AbstractValidator<CreateDirectionDTO>
{
    public CreateDirectionDTOValidator()
    {
        RuleFor(x => x.ParticipantId).NotEmpty().WithMessage("Participant ID is required");
    }
}
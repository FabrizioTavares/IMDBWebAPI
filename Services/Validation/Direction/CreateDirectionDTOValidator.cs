using Domain.DTOs.DirectionDTOs;
using FluentValidation;

namespace Service.Validation.Direction
{
    public class CreateDirectionDTOValidator : AbstractValidator<CreateDirectionDTO>
    {
        public CreateDirectionDTOValidator()
        {
            RuleFor(x => x.ParticipantId).NotEmpty().WithMessage("Participant ID is required");
        }
    }
}

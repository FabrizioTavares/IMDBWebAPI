using FluentValidation;
using Service.Abstraction.ParticipantServiceAbstractions.ParticipantDTOs;

namespace Service.Abstraction.ParticipantServiceAbstractions.ParticipantValidation;

public class UpdateParticipantDTOValidator : AbstractValidator<UpdateParticipantDTO>
{
    public UpdateParticipantDTOValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).WithMessage("Name must be less than 100 characters");
        RuleFor(x => x.Biography).MaximumLength(1000).WithMessage("Biography must be less than 1000 characters");
    }
}
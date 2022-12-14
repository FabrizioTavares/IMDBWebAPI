using FluentValidation;
using Service.Abstraction.ParticipantServiceAbstractions.ParticipantDTOs;

namespace Service.Abstraction.ParticipantServiceAbstractions.ParticipantValidation;

public class CreateParticipantDTOValidator : AbstractValidator<CreateParticipantDTO>
{
    public CreateParticipantDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).MaximumLength(100).WithMessage("Name must be less than 100 characters");
        RuleFor(x => x.Biography).MaximumLength(1000).WithMessage("Biography must be less than 1000 characters");
    }
}
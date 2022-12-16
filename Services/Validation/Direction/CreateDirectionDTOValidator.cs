using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.DirectionDTOs;

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

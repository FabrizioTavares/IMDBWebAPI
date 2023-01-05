using Domain.DTOs.VoteDTOs;
using FluentValidation;

namespace Service.Validation.Vote;

public class CreateVoteDTOValidator : AbstractValidator<CreateVoteDTO>
{
    public CreateVoteDTOValidator()
    {
        RuleFor(vote => vote.Rating)
            .InclusiveBetween(0, 4)
            .WithMessage("Rating must be between 0 stars and 4 stars");
    }
}
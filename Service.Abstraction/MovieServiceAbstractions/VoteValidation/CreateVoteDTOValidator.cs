using FluentValidation;
using Service.Abstraction.MovieServiceAbstractions.VoteDTOs;

namespace Service.Abstraction.MovieServiceAbstractions.VoteValidation;

public class CreateVoteDTOValidator : AbstractValidator<CreateVoteDTO>
{
    public CreateVoteDTOValidator()
    {
        RuleFor(vote => vote.Rating)
            .InclusiveBetween(0, 4)
            .WithMessage("Rating must be between 0 stars and 4 stars");
    }
}
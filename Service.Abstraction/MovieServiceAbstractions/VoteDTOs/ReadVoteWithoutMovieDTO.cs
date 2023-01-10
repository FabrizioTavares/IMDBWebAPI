using Service.Abstraction.UserServiceAbstractions.UserDTOs;

namespace Service.Abstraction.MovieServiceAbstractions.VoteDTOs;

public class ReadVoteWithoutMovieDTO
{
    public virtual ReadUserReferencelessDTO Voter { get; set; } = default!;
    public int Rating { get; set; }
}
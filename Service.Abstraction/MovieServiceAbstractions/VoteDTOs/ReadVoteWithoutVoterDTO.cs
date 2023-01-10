using Service.Abstraction.MovieServiceAbstractions.MovieDTOs;

namespace Service.Abstraction.MovieServiceAbstractions.VoteDTOs;

public class ReadVoteWithoutVoterDTO
{
    public virtual ReadMovieReferencelessDTO Movie { get; set; } = default!;
    public int Rating { get; set; }
}
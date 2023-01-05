using Domain.DTOs.MovieDTOs;

namespace Domain.DTOs.VoteDTOs
{
    public class ReadVoteWithoutVoterDTO
    {
        public virtual ReadMovieReferencelessDTO Movie { get; set; } = default!;
        public int Rating { get; set; }
    }
}
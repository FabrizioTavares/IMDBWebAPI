using Domain.DTOs.MovieDTOs;
using Domain.DTOs.UserDTOs;

namespace Domain.DTOs.VoteDTOs
{
    public class ReadVoteWithoutMovieDTO
    {
        public virtual ReadUserReferencelessDTO Voter { get; set; } = default!;
        public int Rating { get; set; }
    }
}

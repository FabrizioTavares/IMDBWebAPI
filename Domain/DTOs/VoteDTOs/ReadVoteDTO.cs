using Domain.DTOs.MovieDTOs;
using Domain.DTOs.UserDTOs;

namespace Domain.DTOs.VoteDTOs
{
    public class ReadVoteDTO
    {
        public virtual ReadUserDTO Voter { get; set; } = new();
        public int Rating { get; set; }
    }
}

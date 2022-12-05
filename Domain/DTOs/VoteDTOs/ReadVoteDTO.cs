using Domain.Models;

namespace Domain.DTOs.VoteDTOs
{
    public class ReadVoteDTO
    {
        public virtual User Voter { get; set; } = new();
        public int VoterId { get; set; }
        public virtual Movie Movie { get; set; } = new();
        public int MovieId { get; set; }
        public int Rating { get; set; }
    }
}

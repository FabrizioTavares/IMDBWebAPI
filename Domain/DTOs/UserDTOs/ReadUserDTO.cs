using Domain.DTOs.VoteDTOs;

namespace Domain.DTOs.UserDTOs
{
    public class ReadUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public ICollection<ReadVoteWithoutVoterDTO> Votes { get; set; } = new List<ReadVoteWithoutVoterDTO>();
    }
}

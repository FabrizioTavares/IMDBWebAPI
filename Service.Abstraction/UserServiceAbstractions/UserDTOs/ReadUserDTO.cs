using Service.Abstraction.MovieServiceAbstractions.VoteDTOs;

namespace Service.Abstraction.UserServiceAbstractions.UserDTOs;

public class ReadUserDTO
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public ICollection<ReadVoteWithoutVoterDTO> Votes { get; set; } = new List<ReadVoteWithoutVoterDTO>();
}
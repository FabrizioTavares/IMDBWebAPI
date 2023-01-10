using Service.Abstraction.MovieServiceAbstractions.MovieDTOs;

namespace Service.Abstraction.MovieServiceAbstractions.DirectionDTOs;

public class ReadDirectionFromParticipantDTO
{
    public virtual ReadMovieReferencelessDTO Movie { get; set; } = new();
}
using Service.Abstraction.MovieServiceAbstractions.MovieDTOs;

namespace Service.Abstraction.MovieServiceAbstractions.PerformanceDTOs;

public class ReadPerformanceFromParticipantDTO
{
    public string? CharacterName { get; set; }
    public virtual ReadMovieReferencelessDTO? Movie { get; set; }
}
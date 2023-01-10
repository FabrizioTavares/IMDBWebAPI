using Service.Abstraction.ParticipantServiceAbstractions.ParticipantDTOs;

namespace Service.Abstraction.MovieServiceAbstractions.PerformanceDTOs;

public class ReadPerformanceFromMovieDTO
{
    public string CharacterName { get; set; } = string.Empty;
    public virtual ReadParticipantReferencelessDTO? Actor { get; set; }
}
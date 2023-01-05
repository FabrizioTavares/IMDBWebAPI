using Domain.DTOs.ParticipantDTOs;

namespace Domain.DTOs.PerformanceDTOs;

public class ReadPerformanceFromMovieDTO
{
    public string CharacterName { get; set; } = string.Empty;
    public virtual ReadParticipantReferencelessDTO? Actor { get; set; }
}
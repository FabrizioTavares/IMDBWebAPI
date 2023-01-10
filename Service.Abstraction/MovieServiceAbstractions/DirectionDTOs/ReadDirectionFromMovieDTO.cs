using Service.Abstraction.ParticipantServiceAbstractions.ParticipantDTOs;

namespace Service.Abstraction.MovieServiceAbstractions.DirectionDTOs;

public class ReadDirectionFromMovieDTO
{
    public virtual ReadParticipantReferencelessDTO Participant { get; set; } = new();
}
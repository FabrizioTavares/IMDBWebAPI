using Domain.DTOs.MovieDTOs;
using Domain.DTOs.ParticipantDTOs;

namespace Domain.DTOs.DirectionDTOs
{
    public class ReadDirectionFromMovieDTO
    {
        public virtual ReadParticipantReferencelessDTO Participant { get; set; } = new();
    }
}

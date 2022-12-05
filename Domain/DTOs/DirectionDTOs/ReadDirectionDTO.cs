using Domain.DTOs.MovieDTOs;
using Domain.DTOs.ParticipantDTOs;

namespace Domain.DTOs.DirectionDTOs
{
    public class ReadDirectionDTO
    {
        public int MovieId { get; set; }
        public virtual ReadMovieDTO Movie { get; set; } = new();
        public int ParticipantId { get; set; }
        public virtual ReadParticipantDTO Participant { get; set; } = new();
    }
}

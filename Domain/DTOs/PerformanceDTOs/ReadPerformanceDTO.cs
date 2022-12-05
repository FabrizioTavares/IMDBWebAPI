using Domain.DTOs.MovieDTOs;
using Domain.DTOs.ParticipantDTOs;

namespace Domain.DTOs.PerformanceDTOs
{
    public class ReadPerformanceDTO
    {
        public string? CharacterName { get; set; }
        public int MovieId { get; set; }
        public virtual ReadMovieDTO Movie { get; set; } = new();
        public int ParticipantId { get; set; }
        public virtual ReadParticipantDTO Participant { get; set; } = new();
    }
}

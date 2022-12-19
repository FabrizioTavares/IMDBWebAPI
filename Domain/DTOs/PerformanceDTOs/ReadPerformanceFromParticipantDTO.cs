using Domain.DTOs.MovieDTOs;

namespace Domain.DTOs.PerformanceDTOs
{
    public class ReadPerformanceFromParticipantDTO
    {
        public string? CharacterName { get; set; }
        public virtual ReadMovieReferencelessDTO? Movie { get; set; }
    }
}

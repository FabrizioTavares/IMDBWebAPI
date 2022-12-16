using Domain.DTOs.MovieDTOs;
using Domain.DTOs.ParticipantDTOs;

namespace Domain.DTOs.DirectionDTOs
{
    public class ReadDirectionFromParticipantDTO
    {
        public virtual ReadMovieReferencelessDTO Movie { get; set; } = new();
    }
}

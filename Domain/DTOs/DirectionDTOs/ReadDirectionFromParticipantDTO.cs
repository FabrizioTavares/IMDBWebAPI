using Domain.DTOs.MovieDTOs;

namespace Domain.DTOs.DirectionDTOs;

public class ReadDirectionFromParticipantDTO
{
    public virtual ReadMovieReferencelessDTO Movie { get; set; } = new();
}
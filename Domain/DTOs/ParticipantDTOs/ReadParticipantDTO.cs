using Domain.DTOs.MovieDTOs;
using Domain.DTOs.PerformanceDTOs;

namespace Domain.DTOs.ParticipantDTOs;

public class ReadParticipantDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public ICollection<ReadPerformanceFromParticipantDTO>? MoviesActedIn { get; set; }
    public ICollection<ReadMovieReferencelessDTO>? MoviesDirected { get; set; }
}
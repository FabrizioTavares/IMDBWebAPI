using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.PerformanceDTOs;

namespace Domain.DTOs.ParticipantDTOs
{
    public class ReadParticipantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public ICollection<ReadPerformanceDTO>? MoviesActedIn { get; set; }
        public ICollection<ReadDirectionDTO>? MoviesDirected { get; set; }
    }
}

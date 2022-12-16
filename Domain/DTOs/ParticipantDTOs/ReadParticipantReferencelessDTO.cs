using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.PerformanceDTOs;

namespace Domain.DTOs.ParticipantDTOs
{
    public class ReadParticipantReferencelessDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

namespace Domain.DTOs.PerformanceDTOs
{
    public class CreatePerformanceDTO
    {
        public string? CharacterName { get; set; }
        public int MovieId { get; set; }
        public int ParticipantId { get; set; }
    }
}

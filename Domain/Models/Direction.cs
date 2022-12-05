namespace Domain.Models
{
    public class Direction
    {
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; } = new();
        public int ParticipantId { get; set; }
        public virtual Participant Participant { get; set; } = new();
    }
}

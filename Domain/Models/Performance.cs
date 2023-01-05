namespace Domain.Models;

public class Performance
{
    public string CharacterName { get; set; } = "As him/herself";
    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; } = default!;
    public int ParticipantId { get; set; }
    public virtual Participant Participant { get; set; } = default!;

}
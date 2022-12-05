using Domain.Models.Abstract;

namespace Domain.Models
{
    public class Participant : IdentifiableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public virtual ICollection<Performance>? MoviesActedIn { get; set; }
        public virtual ICollection<Direction>? MoviesDirected { get; set; }
    }

}

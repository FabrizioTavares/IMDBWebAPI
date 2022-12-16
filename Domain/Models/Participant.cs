using Domain.Models.Abstract;

namespace Domain.Models
{
    public class Participant : IdentifiableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public virtual List<Performance> MoviesActedIn { get; set; } = new List<Performance>();
        public virtual List<Direction> MoviesDirected { get; set; } = new List<Direction>();
    }

}

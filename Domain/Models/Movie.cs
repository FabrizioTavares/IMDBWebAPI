using Domain.Models.Abstract;

namespace Domain.Models
{
    public class Movie : IdentifiableEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; } = null;
        public int? Duration { get; set; } = 0;
        public virtual List<Vote> Votes { get; set; } = new List<Vote>();
        public virtual List<Genre> Genres { get; set; } = new List<Genre>();
        public virtual List<Performance> Cast { get; set; } = new List<Performance>();
        public virtual List<Direction> Direction { get; set; } = new List<Direction>();
    }
}

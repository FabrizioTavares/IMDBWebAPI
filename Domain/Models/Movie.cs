using Domain.Models.Abstract;

namespace Domain.Models
{
    public class Movie : IdentifiableEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; } = null;
        public int? Duration { get; set; } = 0;
        public ICollection<Vote>? Votes { get; set; } = Array.Empty<Vote>();
        public virtual ICollection<Genre> Genres { get; set; } = Array.Empty<Genre>();
        public virtual ICollection<Performance>? Cast { get; set; } = Array.Empty<Performance>();
        public virtual ICollection<Direction>? Direction { get; set; } = Array.Empty<Direction>();
    }
}

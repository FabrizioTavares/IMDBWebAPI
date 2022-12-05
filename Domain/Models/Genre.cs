using Domain.Models.Abstract;

namespace Domain.Models
{
    public class Genre : IdentifiableEntity
    {
        public string Title { get; set; } = string.Empty;
        public virtual ICollection<Movie>? Movies { get; set; } = Array.Empty<Movie>();
    }
}

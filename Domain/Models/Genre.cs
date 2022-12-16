using Domain.Models.Abstract;

namespace Domain.Models
{
    public class Genre : IdentifiableEntity
    {
        public string Title { get; set; } = string.Empty;
        public virtual List<Movie> Movies { get; set; } = new List<Movie>();
    }
}

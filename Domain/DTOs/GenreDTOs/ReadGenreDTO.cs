using Domain.DTOs.MovieDTOs;

namespace Domain.DTOs.GenreDTOs
{
    public class ReadGenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<ReadMovieDTO>? Movies { get; set; }
    }
}

using Domain.DTOs.MovieDTOs;

namespace Domain.DTOs.GenreDTOs;

public class ReadGenreDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public virtual ICollection<ReadMovieReferencelessDTO>? Movies { get; set; }
}
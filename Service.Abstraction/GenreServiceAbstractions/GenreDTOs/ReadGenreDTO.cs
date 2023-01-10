using Service.Abstraction.MovieServiceAbstractions.MovieDTOs;

namespace Service.Abstraction.GenreServiceAbstractions.GenreDTOs;

public class ReadGenreDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public virtual List<ReadMovieReferencelessDTO>? Movies { get; set; }
}
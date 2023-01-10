namespace Service.Abstraction.MovieServiceAbstractions.MovieDTOs;

public class UpdateMovieDTO
{
    public string? Title { get; set; }
    public string? Synopsis { get; set; }
    public int? ReleaseYear { get; set; }
    public int? Duration { get; set; }
}
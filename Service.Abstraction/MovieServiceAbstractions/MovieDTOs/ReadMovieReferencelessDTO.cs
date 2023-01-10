namespace Service.Abstraction.MovieServiceAbstractions.MovieDTOs;

public class ReadMovieReferencelessDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Synopsis { get; set; } = string.Empty;
    public int? ReleaseYear { get; set; } = null;
    public int? Duration { get; set; } = 0;
    public int? Quantity_Votes { get; set; } = 0;
    public double? Rating { get; set; } = 0;
}
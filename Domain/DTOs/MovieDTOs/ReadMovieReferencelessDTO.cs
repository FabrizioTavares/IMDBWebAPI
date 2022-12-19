namespace Domain.DTOs.MovieDTOs
{
    public class ReadMovieReferencelessDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; } = null;
        public int? Duration { get; set; } = 0;
    }
}

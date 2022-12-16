using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.GenreDTOs;
using Domain.DTOs.PerformanceDTOs;

namespace Domain.DTOs.MovieDTOs
{
    public class UpdateMovieDTO
    {
        public string? Title { get; set; }
        public string? Synopsis { get; set; }
        public int? ReleaseYear { get; set; }
        public int? Duration { get; set; }
    }
}

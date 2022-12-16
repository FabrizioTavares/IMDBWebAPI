using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.GenreDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.Models;

namespace Domain.DTOs.MovieDTOs
{
    public class CreateMovieDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; }
        public int Duration { get; set; }
    }
}

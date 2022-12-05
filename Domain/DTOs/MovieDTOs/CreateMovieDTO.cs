using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.PerformanceDTOs;

namespace Domain.DTOs.MovieDTOs
{
    public class CreateMovieDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; }
        public int Duration { get; set; }
        public virtual ICollection<int> Genres { get; set; } = Array.Empty<int>();
        public virtual ICollection<CreatePerformanceDTO> Performances { get; set; } = Array.Empty<CreatePerformanceDTO>();
        public virtual ICollection<CreateDirectionDTO> Directions { get; set; } = Array.Empty<CreateDirectionDTO>();
    }
}

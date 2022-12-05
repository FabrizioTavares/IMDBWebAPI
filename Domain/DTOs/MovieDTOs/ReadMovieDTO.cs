using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.GenreDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.DTOs.VoteDTOs;

namespace Domain.DTOs.MovieDTOs
{
    public class ReadMovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; } = null;
        public int Duration { get; set; } = 0;
        public ICollection<ReadVoteDTO>? Votes { get; set; } = Array.Empty<ReadVoteDTO>();
        public virtual ICollection<ReadGenreDTO> Genres { get; set; } = Array.Empty<ReadGenreDTO>();
        public virtual ICollection<ReadPerformanceDTO> Cast { get; set; } = Array.Empty<ReadPerformanceDTO>();
        public virtual ICollection<ReadDirectionDTO> Direction { get; set; } = Array.Empty<ReadDirectionDTO>();
    }
}

using Domain.DTOs.GenreDTOs;
using Domain.DTOs.ParticipantDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.DTOs.VoteDTOs;

namespace Domain.DTOs.MovieDTOs
{
    public class ReadMovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; } = 1888;
        public int Duration { get; set; } = 0;
        public virtual IEnumerable<ReadVoteDTO>? Votes { get; set; }
        public virtual IEnumerable<ReadGenreReferencelessDTO>? Genres { get; set; }
        public virtual IEnumerable<ReadPerformanceFromMovieDTO>? Cast { get; set; }
        public virtual IEnumerable<ReadParticipantReferencelessDTO>? Direction { get; set; }
    }
}

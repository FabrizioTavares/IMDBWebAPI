using Service.Abstraction.GenreServiceAbstractions.GenreDTOs;
using Service.Abstraction.MovieServiceAbstractions.PerformanceDTOs;
using Service.Abstraction.MovieServiceAbstractions.VoteDTOs;
using Service.Abstraction.ParticipantServiceAbstractions.ParticipantDTOs;

namespace Service.Abstraction.MovieServiceAbstractions.MovieDTOs;

public class ReadMovieDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Synopsis { get; set; } = string.Empty;
    public int? ReleaseYear { get; set; } = 1888;
    public int Duration { get; set; } = 0;
    public int Quantity_Votes { get; set; } = 0;
    public double Rating { get; set; } = 0;
    public virtual IEnumerable<ReadVoteWithoutMovieDTO>? Votes { get; set; }
    public virtual IEnumerable<ReadGenreReferencelessDTO>? Genres { get; set; }
    public virtual IEnumerable<ReadPerformanceFromMovieDTO>? Cast { get; set; }
    public virtual IEnumerable<ReadParticipantReferencelessDTO>? Direction { get; set; }
}
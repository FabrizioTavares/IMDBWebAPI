using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.MovieDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.DTOs.VoteDTOs;

namespace Service.Services.Abstract
{
    public interface IMovieService
    {
        Task<ReadMovieDTO?> Get(int id, CancellationToken cancellationToken);
        IEnumerable<ReadMovieReferencelessDTO?> GetMovies(
            bool sortedByTitle = false,
            bool sortedByRating = false,
            string? title = null,
            string? actor = null,
            string? director = null,
            string? genre = null,
            int? pageNumber = null,
            int? pageSize = null
            );
        Task Insert(CreateMovieDTO movie, CancellationToken cancellationToken);
        Task AddPerformanceToMovie(int movieId, CreatePerformanceDTO newPerformance, CancellationToken cancellationToken);
        Task RemovePerformanceFromMovie(int movieId, int participantId, CancellationToken cancellationToken);
        Task AddDirectionToMovie(int movieId, CreateDirectionDTO newDirection, CancellationToken cancellationToken);
        Task RemoveDirectionFromMovie(int movieId, int participantId, CancellationToken cancellationToken);
        Task AddGenreToMovie(int movieId, int genreId, CancellationToken cancellationToken);
        Task AddReviewToMovie(int movieId, int userId, CreateVoteDTO newReview, CancellationToken cancellationToken);
        Task RemoveGenreFromMovie(int movieId, int genreId, CancellationToken cancellationToken);
        Task RemoveReviewFromMovie(int movieId, int userId, CancellationToken cancellationToken);
        Task Update(int id, UpdateMovieDTO movie, CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
    }
}

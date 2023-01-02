using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.MovieDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.DTOs.VoteDTOs;
using FluentResults;

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
        Task<Result> Insert(CreateMovieDTO movie, CancellationToken cancellationToken);
        Task<Result> AddPerformanceToMovie(int movieId, CreatePerformanceDTO newPerformance, CancellationToken cancellationToken);
        Task<Result> RemovePerformanceFromMovie(int movieId, int participantId, CancellationToken cancellationToken);
        Task<Result> AddDirectionToMovie(int movieId, CreateDirectionDTO newDirection, CancellationToken cancellationToken);
        Task<Result> RemoveDirectionFromMovie(int movieId, int participantId, CancellationToken cancellationToken);
        Task<Result> AddGenreToMovie(int movieId, int genreId, CancellationToken cancellationToken);
        Task<Result> AddReviewToMovie(int movieId, int userId, CreateVoteDTO newReview, CancellationToken cancellationToken);
        Task<Result> RemoveGenreFromMovie(int movieId, int genreId, CancellationToken cancellationToken);
        Task<Result> RemoveReviewFromMovie(int movieId, int userId, CancellationToken cancellationToken);
        Task<Result> Update(int id, UpdateMovieDTO movie, CancellationToken cancellationToken);
        Task<Result> Remove(int id, CancellationToken cancellationToken);
    }
}

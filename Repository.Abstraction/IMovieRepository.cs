using Domain.Models;

namespace Repository.Abstraction;

public interface IMovieRepository : IBaseRepository<Movie>
{
    IEnumerable<Movie?> GetMovies(
        bool sortedByTitle = false,
        bool sortedByRating = false,
        string? title = null,
        string? actor = null,
        string? director = null,
        string? genre = null,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default
        );

}
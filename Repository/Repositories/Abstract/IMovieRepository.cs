using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        IEnumerable<Movie?> GetMoviesByTitle(string title, CancellationToken cancellationToken);
        IEnumerable<Movie?> GetMoviesByActor(string actorName, CancellationToken cancellationToken);
        IEnumerable<Movie?> GetMoviesByDirector(string directorName, CancellationToken cancellationToken);
        IEnumerable<Movie?> GetMoviesByGenre(string genreName, CancellationToken cancellationToken);

        // TODO: Sort movies by average rating
    }
}

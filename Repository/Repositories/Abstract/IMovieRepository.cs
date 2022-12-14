using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        IEnumerable<Movie?> GetMoviesByTitle(string title, CancellationToken cancellationToken);

        // TODO IMPLEMENT MORE FILTERS AND QUERIES
    }
}

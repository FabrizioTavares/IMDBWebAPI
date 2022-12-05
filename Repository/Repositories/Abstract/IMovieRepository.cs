using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        Task<Movie?> GetMovieByTitle(string title, CancellationToken cancellationToken);

        // TODO IMPLEMENT MORE FILTERS AND QUERIES
    }
}

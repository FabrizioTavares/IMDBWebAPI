using Domain.Models;

namespace Repository.Repositories.Abstract
{
    internal interface IMovieRepository : IBaseRepository<Movie>
    {
        Task<Movie?> GetMovieByTitle(string title, CancellationToken cancellationToken);

        // TODO IMPLEMENT MORE FILTERS AND QUERIES
    }
}

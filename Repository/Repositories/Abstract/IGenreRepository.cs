using Domain.Models;

namespace Repository.Repositories.Abstract
{
    internal interface IGenreRepository : IBaseRepository<Genre>
    {
        Task<Genre?> GetGenreByTitle(string title, CancellationToken cancellationToken);
    }
}

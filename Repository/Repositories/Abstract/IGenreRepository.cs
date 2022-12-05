using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        Task<Genre?> GetGenreByTitle(string title, CancellationToken cancellationToken);
    }
}

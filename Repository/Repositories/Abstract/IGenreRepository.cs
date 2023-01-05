using Domain.Models;

namespace Repository.Repositories.Abstract;

public interface IGenreRepository : IBaseRepository<Genre>
{
    IEnumerable<Genre> GetGenresByTitle(string title);
}
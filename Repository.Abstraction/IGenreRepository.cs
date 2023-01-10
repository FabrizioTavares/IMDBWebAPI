using Domain.Models;

namespace Repository.Abstraction;

public interface IGenreRepository : IBaseRepository<Genre>
{
    IEnumerable<Genre> GetGenresByTitle(string title);
}
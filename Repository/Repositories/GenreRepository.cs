using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public IEnumerable<Genre> GetGenresByTitle(string title)
        {
            return _entities.Where(g => g.Title.Contains(title));
        }
    }
}

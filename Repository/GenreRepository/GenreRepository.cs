using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using Repository.Data;

namespace Repository.GenreRepository;

public class GenreRepository : BaseRepository<Genre>, IGenreRepository
{
    public GenreRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    public override async Task<Genre?> Get(int id, CancellationToken cancellationToken)
    {
        return await _entities.Where(g => g.Id == id).Include(g => g.Movies).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public IEnumerable<Genre> GetGenresByTitle(string title)
    {
        return _entities.Where(g => g.Title.Contains(title));
    }
}
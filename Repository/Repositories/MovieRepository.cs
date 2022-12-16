using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;
using System.Collections;

namespace Repository.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public IEnumerable<Movie?> GetMoviesByTitle(string title, CancellationToken cancellationToken)
        {
            return _entities.Where(m => m.Title.Contains(title)).Include(m => m.Cast).ThenInclude(c => c.Participant).Include(m => m.Genres).Include(m => m.Direction).AsNoTracking();
        }

        public override Task<Movie?> Get(int id, CancellationToken cancellationToken)
        {
            return _entities
                .Include(m => m.Cast)
                .ThenInclude(c => c.Participant)
                .Include(m => m.Genres)
                .Include(m => m.Direction)
                .ThenInclude(d => d.Participant)
                .AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

    }
}

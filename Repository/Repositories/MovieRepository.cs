using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

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


        public IEnumerable<Movie?> GetMoviesByActor(string actorName, CancellationToken cancellationToken)
        {
            return _entities.Where(m => m.Cast.Any(p => p.Participant.Name.Contains(actorName))).Include(m => m.Cast).ThenInclude(c => c.Participant).Include(m => m.Genres).Include(m => m.Direction).AsNoTracking();
        }

        public IEnumerable<Movie?> GetMoviesByDirector(string directorName, CancellationToken cancellationToken)
        {
            return _entities.Where(m => m.Direction.Any(d => d.Participant.Name.Contains(directorName))).Include(m => m.Cast).ThenInclude(c => c.Participant).Include(m => m.Genres).Include(m => m.Direction).AsNoTracking();
        }
        
        public IEnumerable<Movie?> GetMoviesByGenre(string genreName, CancellationToken cancellationToken)
        {
            return _entities.Where(m => m.Genres.Any(g => g.Title.Contains(genreName))).Include(m => m.Cast).ThenInclude(c => c.Participant).Include(m => m.Genres).Include(m => m.Direction).AsNoTracking();
        }

        public override Task<Movie?> Get(int id, CancellationToken cancellationToken)
        {
            return _entities
                .Include(m => m.Cast)
                .ThenInclude(c => c.Participant)
                .Include(m => m.Genres)
                .Include(m => m.Direction)
                .ThenInclude(d => d.Participant)
                .Include(m => m.Votes)
                .ThenInclude(v => v.User)
                .FirstOrDefaultAsync(cancellationToken);
        }

    }
}

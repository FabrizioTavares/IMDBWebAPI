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

        public virtual async Task<Movie?> GetMovieByTitle(string title, CancellationToken cancellationToken)
        {
            return await _entities.FirstOrDefaultAsync(m => m.Title == title, cancellationToken);
        }
    }
}

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

        public virtual IEnumerable<Movie?> GetMoviesByTitle(string title, CancellationToken cancellationToken)
        {
            return _entities.Where(m => m.Title.Contains(title));
        }
    }
}

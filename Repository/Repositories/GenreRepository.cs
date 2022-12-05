using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    internal class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public async Task<Genre?> GetGenreByTitle(string title, CancellationToken cancellationToken)
        {
            return await _entities.FirstOrDefaultAsync(g => g.Title == title, cancellationToken);
        }
    }
}

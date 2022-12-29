using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class VoteRepository : BaseCompositeRepository<Vote>, IVoteRepository
    {
        public VoteRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public override async Task<Vote?> GetComposite(int movieId, int userId, CancellationToken cancellationToken)
        {
            return await _entities
                .Include(v => v.Movie)
                .Include(v => v.User)
                .FirstAsync(v => v.MovieId == movieId && v.UserId == userId, cancellationToken);
        }

        public async Task<Vote?> GetVotesByRating(int rating, CancellationToken cancellationToken)
        {
            return await _entities
                .Include(v => v.Movie)
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.Rating <= rating, cancellationToken);
        }
    }
}

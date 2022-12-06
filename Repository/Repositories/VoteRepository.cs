using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class VoteRepository : BaseCompositeRepository<Vote>, IVoteRepository
    {
        public VoteRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public override async Task<Vote?> GetComposite(int movieId, int voterId, CancellationToken cancellationToken)
        {
            return await _entities
                .Include(v => v.Movie)
                .Include(v => v.Voter)
                .FirstAsync(v => v.MovieId == movieId || v.VoterId == voterId, cancellationToken);
        }

        public async Task<Vote?> GetVotesByRating(int rating, CancellationToken cancellationToken)
        {
            return await _entities
                .Include(v => v.Movie)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(v => v.Rating < rating, cancellationToken);
        }
    }
}

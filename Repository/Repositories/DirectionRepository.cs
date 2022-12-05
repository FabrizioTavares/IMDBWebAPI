using Domain.Models;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class DirectionRepository : BaseRepository<Direction>, IDirectionRepository
    {
        public DirectionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public override async Task<Direction?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken)
        {
            return await base.GetComposite(movieId, participantId, cancellationToken);
        }
    }
}

using Domain.Models;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class DirectionRepository : BaseCompositeRepository<Direction>, IDirectionRepository
    {
        public DirectionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public override async Task<Direction?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken)
        {
            return await base.GetComposite(movieId, participantId, cancellationToken);
        }

        public IEnumerable<Direction?> GetDirectionsByMovie(int movieId, CancellationToken cancellationToken)
        {
            return _entities.Where(d => d.MovieId == movieId);
        }

        public IEnumerable<Direction?> GetDirectionsByParticipant(int participantId, CancellationToken cancellationToken)
        {
            return _entities.Where(d => d.ParticipantId == participantId);
        }
    }
}

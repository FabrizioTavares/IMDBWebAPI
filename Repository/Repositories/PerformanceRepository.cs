using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class PerformanceRepository : BaseCompositeRepository<Performance>, IPerformanceRepository
    {
        public PerformanceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<Performance?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken)
        {
            return await base.GetComposite(movieId, participantId, cancellationToken);
        }

        public IEnumerable<Performance> GetByCharacterName(string name, CancellationToken cancellationToken)
        {
            return _entities.Where(p => p.CharacterName != null && p.CharacterName.Contains(name));
        }

        public IEnumerable<Performance?> GetPerformancesByMovie(int movieId, CancellationToken cancellationToken)
        {
            return _entities.Where(p => p.MovieId == movieId);
        }

        public IEnumerable<Performance?> GetPerformancesByParticipant(int participantId, CancellationToken cancellationToken)
        {
            return _entities.Where(p => p.ParticipantId == participantId);
        }
    }
}

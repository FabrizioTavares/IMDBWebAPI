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

        public async Task<Performance?> GetPerformanceByCharacterName(string name, CancellationToken cancellationToken)
        {
            return await _entities.FirstOrDefaultAsync(p => p.CharacterName == name, cancellationToken);
        }

    }
}

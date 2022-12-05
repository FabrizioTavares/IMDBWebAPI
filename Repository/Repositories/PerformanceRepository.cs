using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    internal class PerformanceRepository : BaseRepository<Performance>, IPerformanceRepository
    {
        public PerformanceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<Performance?> GetPerformanceByCharacterName(string name, CancellationToken cancellationToken)
        {
            return await _entities.FirstOrDefaultAsync(p => p.CharacterName == name, cancellationToken);
        }

    }
}

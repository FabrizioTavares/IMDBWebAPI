using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public virtual async Task<Participant?> GetParticipantByName(string name, CancellationToken cancellationToken)
        {
            return await _entities.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
        }

    }
}

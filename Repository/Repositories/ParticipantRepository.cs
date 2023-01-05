using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories;

public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
{
    public ParticipantRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public override async Task<Participant?> Get(int id, CancellationToken cancellationToken)
    {
        return await _entities
            .Include(p => p.MoviesActedIn).ThenInclude(p => p.Movie)
            .Include(p => p.MoviesDirected).ThenInclude(p => p.Movie)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
    }

    public virtual IEnumerable<Participant> GetParticipantsByName(string name)
    {
        return _entities.Where(p => p.Name.Contains(name));
    }

}
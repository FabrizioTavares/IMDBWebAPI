using Domain.Models;
using Repository.Abstraction;
using Repository.Data;

namespace Repository.DirectionRepository;

public class DirectionRepository : BaseCompositeRepository<Direction>, IDirectionRepository
{
    public DirectionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    public override async Task<Direction?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken)
    {
        return await base.GetComposite(movieId, participantId, cancellationToken);
    }
}
using Domain.Models;
using Repository.Abstraction;
using Repository.Data;

namespace Repository.PerformanceRepository;

public class PerformanceRepository : BaseCompositeRepository<Performance>, IPerformanceRepository
{
    public PerformanceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public override async Task<Performance?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken)
    {
        return await base.GetComposite(movieId, participantId, cancellationToken);
    }
}
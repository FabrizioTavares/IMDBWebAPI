using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IPerformanceRepository : IBaseRepository<Performance>
    {
        Task<Performance?> GetPerformanceByCharacterName(string title, CancellationToken cancellationToken);
    }
}

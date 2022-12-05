using Domain.Models;

namespace Repository.Repositories.Abstract
{
    internal interface IPerformanceRepository : IBaseRepository<Performance>
    {
        Task<Performance?> GetPerformanceByCharacterName(string title, CancellationToken cancellationToken);
    }
}

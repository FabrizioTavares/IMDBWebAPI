using Domain.Models;

namespace Repository.Repositories.Abstract
{
    internal interface IVoteRepository : IBaseRepository<Vote>
    {
        Task<Vote?> GetVotesByRating(int rating, CancellationToken cancellationToken);
    }
}

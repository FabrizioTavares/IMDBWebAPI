using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public IEnumerable<User> GetUsers(
                   bool sortedByName = false,
                   string? name = null,
                   int? pageNumber = null,
                   int? pageSize = null
                   );

        public Task<User?> GetByUserName(string name, CancellationToken cancellationToken);
    }
}

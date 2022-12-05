using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByUserName(string name, CancellationToken cancellationToken);
    }
}

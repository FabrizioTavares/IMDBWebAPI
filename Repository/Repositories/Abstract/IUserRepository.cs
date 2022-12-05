using Domain.Models;

namespace Repository.Repositories.Abstract
{
    internal interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByUserName(string name, CancellationToken cancellationToken);
    }
}

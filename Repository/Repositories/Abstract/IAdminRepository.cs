using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        Task<Admin?> GetAdminByUserName(string userName, CancellationToken cancellationToken);
    }
}

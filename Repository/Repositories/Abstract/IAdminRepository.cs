using Domain.Models;

namespace Repository.Repositories.Abstract
{
    internal interface IAdminRepository : IBaseRepository<Admin>
    {
        Task<Admin?> GetAdminByUserName(string userName, CancellationToken cancellationToken);
    }
}

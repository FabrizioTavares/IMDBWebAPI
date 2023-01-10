using Domain.Models;

namespace Repository.Abstraction;

public interface IAdminRepository : IBaseRepository<Admin>
{
    Task<Admin?> GetAdminByUserName(string userName, CancellationToken cancellationToken);
}
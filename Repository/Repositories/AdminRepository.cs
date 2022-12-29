using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public IEnumerable<Admin> GetAll(int pageNumber, int pageSize)
        {
            return _entities.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public async Task<Admin?> GetAdminByUserName(string userName, CancellationToken cancellationToken)
        {
            return await _entities.FirstOrDefaultAsync(a => a.Username.Contains(userName), cancellationToken);
        }
    }
}

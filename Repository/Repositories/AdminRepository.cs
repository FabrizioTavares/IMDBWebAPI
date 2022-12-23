using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public override async Task<Admin?> Get(int id, CancellationToken cancellationToken)
        {
            return await _entities.FirstOrDefaultAsync(a => a.Id == id && a.IsActive, cancellationToken);
        }

        public override IEnumerable<Admin> GetAll()
        {
            return _entities.Where(a => a.IsActive == true).AsEnumerable();
        }

        public IEnumerable<Admin> GetAll(int pageNumber, int pageSize)
        {
            return _entities.Skip((pageNumber - 1) * pageSize).Take(pageSize).Where(a => a.IsActive == true).AsEnumerable();
        }

        public async Task<Admin?> GetAdminByUserName(string userName, CancellationToken cancellationToken)
        {
            return await _entities.FirstOrDefaultAsync(a => a.Username == userName && a.IsActive, cancellationToken);
        }
    }
}

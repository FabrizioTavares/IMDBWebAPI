using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    public async Task<Admin?> GetAdminByUserName(string userName, CancellationToken cancellationToken)
    {
        return await _entities.FirstOrDefaultAsync(a => a.Username == userName, cancellationToken);
    }
}
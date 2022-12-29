using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public override async Task<User?> Get(int id, CancellationToken cancellationToken)
        {
            return await _entities.Include(u => u.Votes).ThenInclude(v => v.Movie).FirstOrDefaultAsync(u => u.Id == id && u.IsActive, cancellationToken);
        }

        public IEnumerable<User> GetUsers(
            bool sortedByName = false,
            string? name = null,
            int? pageNumber = null,
            int? pageSize = null
            )
        {
            var users = _entities
                .Where(u => u.IsActive)
                .AsQueryable();

            if (name != null)
            {
                users = users.Where(u => u.Username.Contains(name));
            }

            if (pageNumber != null && pageSize != null)
            {
                users = users.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            if (sortedByName)
            {
                users = users.OrderBy(u => u.Username);
            }

            return users;
        }

        public async Task<User?> GetByUserName(string name, CancellationToken cancellationToken)
        {
            return await _entities.FirstOrDefaultAsync(u => u.Username == name && u.IsActive, cancellationToken);
        }

    }
}

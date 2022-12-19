﻿using Domain.Models;
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
            return await _entities.FirstOrDefaultAsync(u => u.Id == id && u.IsActive, cancellationToken);
        }
        
        public IEnumerable<User?> GetUsersByUserName(string name, CancellationToken cancellationToken)
        {
            return _entities.Where(u => u.Username == name && u.IsActive);
        }
    }
}

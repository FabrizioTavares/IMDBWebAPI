﻿using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IEnumerable<User?> GetUsersByUserName(string name, CancellationToken cancellationToken);
        Task<User?> GetUserByUserName(string name, CancellationToken cancellationToken);
        IEnumerable<User?> GetUsersByUserNameOrdered(string name, CancellationToken cancellationToken);
    }
}

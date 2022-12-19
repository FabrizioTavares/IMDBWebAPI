using Domain.DTOs.UserDTOs;

namespace Service.Services.Abstract
{
    public interface IUserService
    {
        Task<ReadUserDTO?> GetUser(int id, CancellationToken cancellationToken);
        IEnumerable<ReadUserDTO?> GetAllUsers();
        Task InsertUser(CredentialsUserDTO newUser, CancellationToken cancellationToken);
        Task UpdateUser(int id, UpdateUserDTO updatedUser, CancellationToken cancellationToken);
        Task RemoveUser(int id, CancellationToken cancellationToken);
    }
}

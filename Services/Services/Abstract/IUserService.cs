using Domain.DTOs.UserDTOs;

namespace Service.Services.Abstract
{
    public interface IUserService
    {
        Task<ReadUserDTO?> GetUser(int id, CancellationToken cancellationToken);
        IEnumerable<ReadUserReferencelessDTO?> GetUsers(
            bool sortedByName = false,
            string? name = null,
            int? pageNumber = null,
            int? pageSize = null
            );
        Task<ReadUserDTO?> GetUserByName(string username, CancellationToken cancellationToken);
        Task InsertUser(CreateUserDTO newUser, CancellationToken cancellationToken);
        Task UpdateUser(int id, UpdateUserDTO updatedUser, CancellationToken cancellationToken);
        Task RemoveUser(int id, CancellationToken cancellationToken);
        Task ToggleUserActivation(int id, CancellationToken cancellationToken);
    }
}

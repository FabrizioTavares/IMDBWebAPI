using FluentResults;
using Service.Abstraction.UserServiceAbstractions.UserDTOs;

namespace Service.Abstraction.UserServiceAbstractions;

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
    Task<Result> InsertUser(CreateUserDTO newUser, CancellationToken cancellationToken);
    Task<Result> UpdateUser(int id, UpdateUserDTO updatedUser, CancellationToken cancellationToken);
    Task<Result> RemoveUser(int id, CancellationToken cancellationToken);
    Task<Result> ToggleUserActivation(int id, CancellationToken cancellationToken);
}
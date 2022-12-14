using FluentResults;
using Service.Abstraction.AdministrativeServiceAbstractions.AdminDTOs;

namespace Service.Abstraction.AdministrativeServiceAbstractions;

public interface IAdministrativeService
{
    Task<ReadAdminDTO?> GetAdmin(int id, CancellationToken cancellationToken);
    IEnumerable<ReadAdminDTO?> GetAllAdmins();
    Task<Result<int>> InsertAdmin(int currentAdminId, CreateAdminDTO newAdmin, CancellationToken cancellationToken);
    Task<Result> UpdateAdmin(int id, int currentAdminId, UpdateAdminDTO updatedAdmin, CancellationToken cancellationToken);
    Task<Result> RemoveAdmin(int id, int currentAdminId, CancellationToken cancellationToken);
    Task<Result<string>> ToggleAdminActivation(int id, int hierarchy, CancellationToken cancellationToken);
}
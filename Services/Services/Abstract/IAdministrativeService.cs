using Domain.DTOs.AdminDTOs;

namespace Service.Services.Abstract
{
    public interface IAdministrativeService
    {
        Task<ReadAdminDTO?> GetAdmin(int id, CancellationToken cancellationToken);
        IEnumerable<ReadAdminDTO?> GetAllAdmins();
        Task InsertAdmin(int currentAdminId, CreateAdminDTO newAdmin, CancellationToken cancellationToken);
        Task UpdateAdmin(int id, int currentAdminId, UpdateAdminDTO updatedAdmin, CancellationToken cancellationToken);
        Task RemoveAdmin(int id, int currentAdminId, CancellationToken cancellationToken);
        Task ToggleAdminActivation(int id, int hierarchy, CancellationToken cancellationToken);
    }
}

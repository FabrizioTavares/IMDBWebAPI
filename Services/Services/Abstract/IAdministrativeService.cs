using Domain.DTOs.AdminDTOs;

namespace Service.Services.Abstract
{
    public interface IAdministrativeService
    {
        Task<ReadAdminDTO?> GetAdmin(int id, CancellationToken cancellationToken);
        IEnumerable<ReadAdminDTO?> GetAllAdmins();
        Task InsertAdmin(CreateAdminDTO newAdmin, CancellationToken cancellationToken);
        Task UpdateAdmin(int id, UpdateAdminDTO updatedAdmin, CancellationToken cancellationToken);
        Task RemoveAdmin(int id, CancellationToken cancellationToken);
    }
}

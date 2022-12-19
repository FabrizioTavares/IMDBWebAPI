using Domain.DTOs.AdminDTOs;
using Domain.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

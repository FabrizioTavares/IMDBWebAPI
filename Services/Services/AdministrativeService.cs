using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.Models;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;

namespace Service.Services
{
    public class AdministrativeService : AuthenticationService, IAdministrativeService
    {

        // TODO IMPLEMENT PASSWORD CHECK AND HIERARCHICAL CHECKS

        private readonly IAdminRepository _adminRepository;

        private readonly IMapper _mapper;

        public AdministrativeService(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public async Task<ReadAdminDTO?> GetAdmin(int id, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.Get(id, cancellationToken);
            return _mapper.Map<ReadAdminDTO>(admin);
        }

        public IEnumerable<ReadAdminDTO?> GetAllAdmins()
        {
            var admins = _adminRepository.GetAll();
            return _mapper.Map<IEnumerable<ReadAdminDTO>>(admins);
        }

        public async Task InsertAdmin(CreateAdminDTO newAdmin, CancellationToken cancellationToken)
        {
            var admin = _mapper.Map<Admin>(newAdmin);
            await _adminRepository.Insert(admin, cancellationToken);
        }

        public async Task RemoveAdmin(int id, CancellationToken cancellationToken)
        {
            var adminToBeRemoved = await _adminRepository.Get(id, cancellationToken);
            if (adminToBeRemoved == null)
            {
                throw new Exception("Admin not found");
            }
            await _adminRepository.Remove(adminToBeRemoved, cancellationToken);
        }

        public async Task UpdateAdmin(int id, UpdateAdminDTO updatedAdmin, CancellationToken cancellationToken)
        {
            var adminToBeUpdated = await _adminRepository.Get(id, cancellationToken);
            if (adminToBeUpdated == null)
            {
                throw new Exception("Admin not found");
            }
            var admin = _mapper.Map<Admin>(updatedAdmin);
        }
    }
}

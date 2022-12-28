using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.Models;
using Domain.Utils.Cryptography;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;
using System.Security;

namespace Service.Services
{
    public class AdministrativeService : IAdministrativeService
    {

        // TODO IMPLEMENT PASSWORD CHECK AND HIERARCHICAL CHECKS

        private readonly IAdminRepository _adminRepository;
        private readonly ICryptographer _cryptographer;
        private readonly IMapper _mapper;

        public AdministrativeService(IAdminRepository adminRepository, ICryptographer cryptographer, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _cryptographer = cryptographer;
            _mapper = mapper;
        }

        // TODO: Requires optimisation - database is accessed twice.
        private async Task CheckHierarchy(int currentAdminId, int targetAdminId, CancellationToken cancellationToken)
        {
            var superior = await _adminRepository.Get(currentAdminId, cancellationToken);
            var targetAdmin = await _adminRepository.Get(targetAdminId, cancellationToken);

            if (superior == null || targetAdmin == null)
                throw new ArgumentException("Admin(s) not found.");

            if (superior!.Hierarchy <= targetAdmin.Hierarchy)
            {
                throw new UnauthorizedAccessException("You do not have the required privileges to perform this action");
            }
        }

        public async Task<ReadAdminDTO?> GetAdmin(int id, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.Get(id, cancellationToken);
            return _mapper.Map<ReadAdminDTO>(admin);
        }

        public IEnumerable<ReadAdminDTO?> GetAllAdmins()
        {
            var admins = _adminRepository.GetAll();
            var alladmins = _mapper.Map<IEnumerable<ReadAdminDTO>>(admins);
            return alladmins;
            //return _mapper.Map<IEnumerable<ReadAdminDTO>>(admins);
        }

        public async Task InsertAdmin(int currentAdminId, CreateAdminDTO newAdmin, CancellationToken cancellationToken)
        {
            var author = await _adminRepository.Get(currentAdminId, cancellationToken);
            var existingAdmin = await _adminRepository.GetAdminByUserName(newAdmin.Username, cancellationToken);

            if (existingAdmin != null)
                throw new ArgumentException("This admin already exists");

            if (author!.Hierarchy <= newAdmin.Hierarchy)
            {
                throw new SecurityException("You cannot create an admin with a higher or equal hierarchy than yours");
            }

            var salt = _cryptographer.GenerateSalt();

            var admin = new Admin
            {
                Username = newAdmin.Username,
                Password = _cryptographer.Hash(newAdmin.Password, salt),
                Salt = salt,
                Hierarchy = newAdmin.Hierarchy,
                IsActive = true
            };

            await _adminRepository.Insert(admin, cancellationToken);
        }

        public async Task RemoveAdmin(int id, int currentAdminId, CancellationToken cancellationToken)
        {
            var author = await _adminRepository.Get(currentAdminId, cancellationToken);

            var adminToBeRemoved = await _adminRepository.Get(id, cancellationToken);

            if (adminToBeRemoved == null)
            {
                throw new ArgumentException("Admin not found");
            }

            if (author!.Id == id)
            {
                throw new SecurityException("You cannot delete your own account.");
            }

            if (author!.Hierarchy <= adminToBeRemoved.Hierarchy)
            {
                throw new UnauthorizedAccessException("You do not have the required privileges to perform this action");
            }

            await _adminRepository.Remove(adminToBeRemoved, cancellationToken);
        }

        public async Task ToggleAdminActivation(int id, int currentAdminId, CancellationToken cancellationToken)
        {

            var author = await _adminRepository.Get(currentAdminId, cancellationToken);

            var adminToBeToggled = await _adminRepository.Get(id, cancellationToken);

            if (adminToBeToggled == null)
            {
                throw new ArgumentException("Admin not found");
            }

            if (author!.Id == id)
            {
                throw new SecurityException("You cannot deactivate your own account.");
            }

            if (author!.Hierarchy <= adminToBeToggled.Hierarchy) // There must be an admin performing this action, so "!" is used.
            {
                throw new UnauthorizedAccessException("You do not have the required privileges to perform this action");
            }

            adminToBeToggled.IsActive = !adminToBeToggled.IsActive;
            await _adminRepository.Update(adminToBeToggled, cancellationToken);
        }

        public async Task UpdateAdmin(int id, int currentAdminId, UpdateAdminDTO dto, CancellationToken cancellationToken)
        {

            var author = await _adminRepository.Get(currentAdminId, cancellationToken);

            var adminToBeUpdated = await _adminRepository.Get(id, cancellationToken);

            if (adminToBeUpdated == null)
            {
                throw new Exception("Admin not found");
            }

            // Admins with higher hierarchy can modify admins with lower hierarchy. Admins can modify themselves.
            if (author!.Hierarchy > adminToBeUpdated.Hierarchy || author!.Id == adminToBeUpdated.Id)
            {
                var mappedAdmin = _mapper.Map(dto, adminToBeUpdated);

                var hashedDTONewPassword = _cryptographer.Hash(dto.NewPassword!, adminToBeUpdated.Salt);
                //var hashedDTOCurrentPassword = _cryptographer.Hash(dto.CurrentPassword!, author.Salt);

                if (!_cryptographer.Verify(dto.CurrentPassword!, author.Password, author.Salt))
                {
                    throw new ArgumentException("Wrong password");
                }

                // check if the password has changed
                if (hashedDTONewPassword != adminToBeUpdated.Password)
                {
                    var newSalt = _cryptographer.GenerateSalt();
                    mappedAdmin.Salt = newSalt;
                    mappedAdmin.Password = _cryptographer.Hash(dto.NewPassword!, newSalt);
                }

                await _adminRepository.Update(mappedAdmin, cancellationToken);
                return;
            }

            throw new UnauthorizedAccessException("You cannot modify an admin with a higher or equal hierarchy than yours");

        }
    }
}

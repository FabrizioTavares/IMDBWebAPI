using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.Models;
using Domain.Utils.Cryptography;
using FluentResults;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;
using Service.Utils.Responses;

namespace Service.Services;

public class AdministrativeService : IAdministrativeService
{

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
    private async Task<bool> CheckHierarchy(int currentAdminId, int targetAdminId, CancellationToken cancellationToken)
    {
        var superior = await _adminRepository.Get(currentAdminId, cancellationToken);
        var targetAdmin = await _adminRepository.Get(targetAdminId, cancellationToken);

        if (superior == null || targetAdmin == null)
            return false;

        if (superior!.Hierarchy <= targetAdmin.Hierarchy)
        {
            return false;
        }

        return true;

    }

    public async Task<ReadAdminDTO?> GetAdmin(int id, CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.Get(id, cancellationToken);
        return _mapper.Map<ReadAdminDTO>(admin);
    }

    public IEnumerable<ReadAdminDTO?> GetAllAdmins()
    {
        var admins = _adminRepository.GetAll();
        var allAdmins = _mapper.Map<IEnumerable<ReadAdminDTO>>(admins);
        return allAdmins;
    }

    public async Task<Result<int>> InsertAdmin(int currentAdminId, CreateAdminDTO newAdmin, CancellationToken cancellationToken)
    {
        var author = await _adminRepository.Get(currentAdminId, cancellationToken);
        var existingAdmin = await _adminRepository.GetAdminByUserName(newAdmin.Username, cancellationToken);

        if (existingAdmin is not null)
            return Result.Fail(new BadRequestError("Admin already exists"));

        if (author!.Hierarchy <= newAdmin.Hierarchy)
        {
            return Result.Fail(new ForbiddenError("You do not have the required permissions to create an admin with this hierarchy"));
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

        var createdAdmin = await _adminRepository.Insert(admin, cancellationToken);

        return Result.Ok(createdAdmin.Id);
    }

    public async Task<Result> RemoveAdmin(int adminTobeRemovedId, int adminPerformingRemovalId, CancellationToken cancellationToken)
    {
        var author = await _adminRepository.Get(adminPerformingRemovalId, cancellationToken);

        var adminToBeRemoved = await _adminRepository.Get(adminTobeRemovedId, cancellationToken);

        if (adminToBeRemoved == null)
        {
            return Result.Fail(new NotFoundError("Admin not found"));
        }

        if (author!.Id == adminTobeRemovedId)
        {
            return Result.Fail(new ForbiddenError("You cannot remove yourself"));
        }

        if (author!.Hierarchy <= adminToBeRemoved.Hierarchy)
        {
            return Result.Fail(new ForbiddenError("You do not have the required permissions to remove this admin"));
        }

        await _adminRepository.Remove(adminToBeRemoved, cancellationToken);
        return Result.Ok();
    }

    public async Task<Result<string>> ToggleAdminActivation(int id, int currentAdminId, CancellationToken cancellationToken)
    {

        var author = await _adminRepository.Get(currentAdminId, cancellationToken);

        var adminToBeToggled = await _adminRepository.Get(id, cancellationToken);

        if (adminToBeToggled == null)
        {
            return Result.Fail(new NotFoundError("Admin not found"));
        }

        if (author!.Id == id)
        {
            return Result.Fail(new ForbiddenError("You cannot deactivate yourself"));
        }

        if (author!.Hierarchy <= adminToBeToggled.Hierarchy)
        {
            return Result.Fail(new ForbiddenError("You do not have the required permissions to deactivate this admin"));
        }

        adminToBeToggled.IsActive = !adminToBeToggled.IsActive;
        await _adminRepository.Update(adminToBeToggled, cancellationToken);
        return Result.Ok($"Admin {adminToBeToggled.Username} is now {(adminToBeToggled.IsActive ? "active" : "inactive")}.");

    }

    public async Task<Result> UpdateAdmin(int id, int currentAdminId, UpdateAdminDTO dto, CancellationToken cancellationToken)
    {

        var author = await _adminRepository.Get(currentAdminId, cancellationToken);

        var adminToBeUpdated = await _adminRepository.Get(id, cancellationToken);

        if (adminToBeUpdated == null)
        {
            return Result.Fail(new NotFoundError("Admin not found"));
        }

        // Admins with higher hierarchy can modify admins with lower hierarchy. Admins can modify themselves, but require a password check.
        if (author!.Hierarchy > adminToBeUpdated.Hierarchy)
        {
            var mappedAdmin = AdminUpdateDTOToClass(dto, adminToBeUpdated);

            await _adminRepository.Update(mappedAdmin, cancellationToken);
            return Result.Ok();
        }

        if (author!.Id == adminToBeUpdated.Id)
        {

            if (!_cryptographer.Verify(dto.CurrentPassword!, author.Password, author.Salt))
            {
                return Result.Fail(new ForbiddenError("Current password is incorrect"));
            }

            var mappedAdmin = AdminUpdateDTOToClass(dto, adminToBeUpdated);

            await _adminRepository.Update(mappedAdmin, cancellationToken);
            return Result.Ok();

        }

        return Result.Fail(new ForbiddenError("You cannot modify an admin with a higher or equal hierarchy than yours"));

    }

    private Admin AdminUpdateDTOToClass(UpdateAdminDTO dto, Admin adminToBeUpdated)
    {
        var mappedAdmin = _mapper.Map(dto, adminToBeUpdated);

        var hashedDTONewPassword = _cryptographer.Hash(dto.NewPassword!, adminToBeUpdated.Salt);

        // check if the password has changed
        if (hashedDTONewPassword != adminToBeUpdated.Password)
        {
            var newSalt = _cryptographer.GenerateSalt();
            mappedAdmin.Salt = newSalt;
            mappedAdmin.Password = _cryptographer.Hash(dto.NewPassword!, newSalt);
        }

        return mappedAdmin;
    }
}
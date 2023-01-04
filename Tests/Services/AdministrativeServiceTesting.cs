using AutoMapper;
using Domain.AutomapperProfiles;
using Domain.DTOs.AdminDTOs;
using Domain.Models;
using Domain.Utils.Cryptography;
using NSubstitute;
using Repository.Repositories.Abstract;
using Service.Services;
using Service.Utils.Responses;
using System.Text;

namespace Tests.Services;

public class AdministrativeServiceTesting
{
    private readonly IMapper _mapper;
    private readonly IAdminRepository _adminRepository;
    private readonly ICryptographer _cryptographer;
    private readonly List<Admin> _context;
    private readonly AdministrativeService _sut;
    private static readonly byte[] _commonSalt = Encoding.ASCII.GetBytes("salt");
    private static readonly byte[] _commonPassword = Encoding.ASCII.GetBytes("password");

    public AdministrativeServiceTesting()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AdminProfile());
        });

        _adminRepository = Substitute.For<IAdminRepository>();
        _mapper = Substitute.For<IMapper>();
        _cryptographer = Substitute.For<ICryptographer>();

        _sut = new AdministrativeService(_adminRepository, _cryptographer, _mapper);

        _context = new List<Admin>
        {
            new Admin { Id = 1, Username = "Gamma", Password = _commonPassword, Salt = _commonSalt, IsActive = true, Hierarchy = 100 },
            new Admin { Id = 2, Username = "Alpha", Password = _commonPassword, Salt = _commonSalt, IsActive = true, Hierarchy = 50 },
            new Admin { Id = 3, Username = "Beta", Password = _commonPassword, Salt = _commonSalt, IsActive = false, Hierarchy = 0 }
        };
    }

    [Fact]
    public void GetAll_WhenCalled_ReturnsAllActiveAdmins()
    {
        // Arrange
        _adminRepository.GetAll()
            .Returns(_context);
        _mapper.Map<IEnumerable<ReadAdminDTO>>(_context)
            .Returns(new List<ReadAdminDTO>
            {
                new ReadAdminDTO { Id = 1, Username = "Gamma"},
                new ReadAdminDTO { Id = 2, Username = "Alpha"}
            });

        // Act
        var result = _sut.GetAllAdmins();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Gamma", result.First()!.Username);
        Assert.Equal("Alpha", result.Last()!.Username);
    }

    [Fact]
    public async void GetAdmin_WhenCalled_ReturnsActiveAdmin()
    {
        // Arrange
        _adminRepository.Get(1, CancellationToken.None)
            .Returns(_context.First());
        _mapper.Map<ReadAdminDTO>(_context.First())
            .Returns(new ReadAdminDTO
            {
                Id = 1,
                Username = "Gamma"
            });

        // Act
        var result = await _sut.GetAdmin(1, CancellationToken.None);

        // Assert
        Assert.Equal("Gamma", result!.Username);
    }

    [Fact]
    public async void Insert_WhenUsernameIsUniqueAndHierarchyIsValid_ReturnsIndex()
    {
        // Arrange
        var newAdmin = new CreateAdminDTO
        {   
            Username = "Delta",
            Password = "password",
            Hierarchy = 30
        };

        var admin = _context.First();

        _adminRepository.Get(admin.Id, CancellationToken.None)
            .Returns(admin);
        _adminRepository.GetAdminByUserName("Delta", CancellationToken.None)
            .Returns((Admin?)null);
        _adminRepository.Insert(Arg.Any<Admin>(), CancellationToken.None)
            .Returns(new Admin
            {
                Id = 4,
                Username = "Delta",
                Password = _commonPassword,
                Salt = _commonSalt,
                IsActive = true,
                Hierarchy = 30
            });
        _cryptographer.GenerateSalt()
            .Returns(_commonSalt);
        _cryptographer.Hash(newAdmin.Password, _commonSalt)
            .Returns(_commonPassword);

        // Act
        var result = await _sut.InsertAdmin(admin.Id, newAdmin, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(4, result.Value);
    }

    [Fact]
    public async void Insert_WhenUsernameIsTaken_ReturnsBadRequestError()
    {
        // Arrange
        var newAdmin = new CreateAdminDTO
        {
            Username = "Alpha",
            Password = "password",
            Hierarchy = 30
        };

        var admin = _context.First();

        _adminRepository.Get(admin.Id, CancellationToken.None)
            .Returns(admin);
        _adminRepository.GetAdminByUserName("Alpha", CancellationToken.None)
            .Returns(_context[1]);

        // Act
        var result = await _sut.InsertAdmin(admin.Id, newAdmin, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
    }

    [Fact]
    public async void Insert_WhenHierarchyIsTooHigh_ReturnsForbiddenError()
    {
        // Arrange
        var newAdmin = new CreateAdminDTO
        {
            Username = "Delta",
            Password = "password",
            Hierarchy = 200
        };

        var admin = _context.First();

        _adminRepository.Get(admin.Id, CancellationToken.None)
            .Returns(admin);
        _adminRepository.GetAdminByUserName("Delta", CancellationToken.None)
            .Returns((Admin?)null);

        // Act
        var result = await _sut.InsertAdmin(admin.Id, newAdmin, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<ForbiddenError>(result.Errors.First());
    }

    [Fact]
    public async void Remove_WhenRemovingLowerHierarchyAdmin_ShouldReturnSuccess()
    {
        // Arrange
        var superiorAdmin = _context[0];
        var inferiorAdmin = _context[1];

        _adminRepository.Get(superiorAdmin.Id, CancellationToken.None)
            .Returns(superiorAdmin);
        _adminRepository.Get(inferiorAdmin.Id, CancellationToken.None)
            .Returns(inferiorAdmin);

        // Act
        var result = await _sut.RemoveAdmin(inferiorAdmin.Id, superiorAdmin.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Remove_WhenRemovingHigherHierarchyAdmin_ShouldReturnForbiddenError()
    {
        // Arrange
        var inferiorAdmin = _context[1];
        var superiorAdmin = _context[0];

        _adminRepository.Get(superiorAdmin.Id, CancellationToken.None)
            .Returns(superiorAdmin);
        _adminRepository.Get(inferiorAdmin.Id, CancellationToken.None)
            .Returns(inferiorAdmin);

        // Act
        var result = await _sut.RemoveAdmin(superiorAdmin.Id, inferiorAdmin.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<ForbiddenError>(result.Errors.First());
        Assert.Equal("You do not have the required permissions to remove this admin", result.Errors.First().Message);
    }

    [Fact]
    public async void Remove_WhenRemovingOneself_ShouldReturnForbiddenError()
    {
        // Arrange
        var admin = _context[0];

        _adminRepository.Get(admin.Id, CancellationToken.None)
            .Returns(admin);

        // Act
        var result = await _sut.RemoveAdmin(admin.Id, admin.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<ForbiddenError>(result.Errors.First());
        Assert.Equal("You cannot remove yourself", result.Errors.First().Message);
    }

    [Fact]
    public async void Remove_WhenAdminToBeRemovedIsInvalid_ShouldReturnNotFoundError()
    {
        // Arrange
        var admin = _context[0];

        _adminRepository.Get(admin.Id, CancellationToken.None)
            .Returns(admin);
        _adminRepository.Get(2, CancellationToken.None)
            .Returns((Admin?)null);

        // Act
        var result = await _sut.RemoveAdmin(2, admin.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<NotFoundError>(result.Errors.First());
        Assert.Equal("Admin not found", result.Errors.First().Message);
    }

    [Fact]
    public async void ToggleAdminActivation_WhenHierarchyAndIdisValid_ShouldReturnSuccess()
    {
        // Arrange
        var superiorAdmin = _context[0];
        var inferiorAdmin = _context[1];

        _adminRepository.Get(superiorAdmin.Id, CancellationToken.None)
            .Returns(superiorAdmin);
        _adminRepository.Get(inferiorAdmin.Id, CancellationToken.None)
            .Returns(inferiorAdmin);

        // Act
        var result = await _sut.ToggleAdminActivation(inferiorAdmin.Id, superiorAdmin.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void ToggleAdminActivation_WhenTogglingHigherHierarchy_ShouldReturnForbiddenError()
    {
        // Arrange
        var superiorAdmin = _context[0];
        var inferiorAdmin = _context[1];

        _adminRepository.Get(superiorAdmin.Id, CancellationToken.None)
            .Returns(superiorAdmin);
        _adminRepository.Get(inferiorAdmin.Id, CancellationToken.None)
            .Returns(inferiorAdmin);

        // Act
        var result = await _sut.ToggleAdminActivation(superiorAdmin.Id, inferiorAdmin.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<ForbiddenError>(result.Errors.First());
        Assert.Equal("You do not have the required permissions to deactivate this admin", result.Errors.First().Message);
    }

    [Fact]
    public async void ToggleAdminActivation_WhenTogglingOneSelf_ShouldReturnForbiddenError()
    {
        // Arrange
        var admin = _context[0];

        _adminRepository.Get(admin.Id, CancellationToken.None)
            .Returns(admin);

        // Act
        var result = await _sut.ToggleAdminActivation(admin.Id, admin.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<ForbiddenError>(result.Errors.First());
        Assert.Equal("You cannot deactivate yourself", result.Errors.First().Message);
    }

   [Fact]
   public async void ToggleAdminActivation_WhenToggledAdminDoesNotExists_ShouldReturnNotFoundError()
    {
        // Arrange
        var admin = _context[0];

        _adminRepository.Get(admin.Id, CancellationToken.None)
            .Returns(admin);
        _adminRepository.Get(2, CancellationToken.None)
            .Returns((Admin?)null);

        // Act
        var result = await _sut.ToggleAdminActivation(2, admin.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<NotFoundError>(result.Errors.First());
        Assert.Equal("Admin not found", result.Errors.First().Message);
    }

    [Fact]
    public async void UpdateAdmin_WhenUpdatingAnotherAdmin_ShouldReturnSuccess()
    {
        // Arrange
        var loggedAdmin = _context[0];

        var updateDTO = new UpdateAdminDTO
        {
            Username = "Zeta",
            NewPassword = "newPassword"
        };

        _adminRepository.Get(loggedAdmin.Id, CancellationToken.None)
            .Returns(loggedAdmin);
        _adminRepository.Get(_context[1].Id, CancellationToken.None)
            .Returns(_context[1]);
        _mapper.Map(updateDTO, _context[1])
            .Returns(new Admin
            {
                Id = 2,
                Username = "Zeta",
                Password = _commonPassword,
                Salt = _commonSalt,
                Hierarchy = 50,
                IsActive = true
            });
        _cryptographer.Hash(Arg.Any<string>(), Arg.Any<byte[]>())
            .Returns(_commonPassword);
        _cryptographer.GenerateSalt().Returns(_commonSalt);


        // Act
        var result = await _sut.UpdateAdmin(_context[1].Id, loggedAdmin.Id, updateDTO, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async void UpdateAdmin_WhenUpdatingOneSelfWithValidPassword_ShouldReturnSuccess()
    {
        // Arrange
        var loggedAdmin = _context[0];

        var updateDTO = new UpdateAdminDTO
        {
            Username = "Zeta",
            CurrentPassword = "Password",
            NewPassword = "newPassword"
        };

        _adminRepository.Get(loggedAdmin.Id, CancellationToken.None)
            .Returns(loggedAdmin);
        _mapper.Map(updateDTO, loggedAdmin)
            .Returns(new Admin
            {
                Id = _context[0].Id,
                Username = "Zeta",
                Password = _commonPassword,
                Salt = _commonSalt,
                Hierarchy = _context[0].Id,
                IsActive = true
            });
        _cryptographer.Hash(Arg.Any<string>(), Arg.Any<byte[]>())
            .Returns(_commonPassword);
        _cryptographer.GenerateSalt().Returns(_commonSalt);
        _cryptographer.Verify(updateDTO.CurrentPassword, loggedAdmin.Password, loggedAdmin.Salt)
            .Returns(true);

        // Act
        var result = await _sut.UpdateAdmin(loggedAdmin.Id, loggedAdmin.Id, updateDTO, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        
    }

    [Fact]
    public async void UpdateAdmin_WhenUpdatingOneSelfWithInvalidPassword_ShouldReturnForbiddenError()
    {
        // Arrange
        var loggedAdmin = _context[0];

        var updateDTO = new UpdateAdminDTO
        {
            Username = "Zeta",
            CurrentPassword = "WrongPassword",
            NewPassword = "newPassword"
        };

        _adminRepository.Get(loggedAdmin.Id, CancellationToken.None)
            .Returns(loggedAdmin);
        _cryptographer.Verify(updateDTO.CurrentPassword, loggedAdmin.Password, loggedAdmin.Salt)
            .Returns(false);

        // Act
        var result = await _sut.UpdateAdmin(loggedAdmin.Id, loggedAdmin.Id, updateDTO, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<ForbiddenError>(result.Errors.First());
        Assert.Equal("Current password is incorrect", result.Errors.First().Message);
    }

    [Fact]
    public async void UpdateAdmin_WhenAttemptingToUpdateAdminWithHigherHierarchy_ShouldReturnForbiddenError()
    {
        // Arrange
        var loggedAdmin = _context[1];
        var superiorAdmin = _context[0];

        var updateDTO = new UpdateAdminDTO
        {
            Username = "Zeta",
            NewPassword = "newPassword"
        };

        _adminRepository.Get(loggedAdmin.Id, CancellationToken.None)
            .Returns(loggedAdmin);
        _adminRepository.Get(superiorAdmin.Id, CancellationToken.None)
            .Returns(superiorAdmin);

        // Act
        var result = await _sut.UpdateAdmin(superiorAdmin.Id, loggedAdmin.Id, updateDTO, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<ForbiddenError>(result.Errors.First());
        Assert.Equal("You cannot modify an admin with a higher or equal hierarchy than yours", result.Errors.First().Message);
    }

}

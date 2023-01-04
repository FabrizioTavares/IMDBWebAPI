using AutoMapper;
using Domain.AutomapperProfiles;
using Domain.DTOs.UserDTOs;
using Domain.Models;
using Domain.Utils.Cryptography;
using NSubstitute;
using Repository.Repositories.Abstract;
using Service.Services;
using Service.Utils.Responses;
using System.Text;

namespace Tests.Services;

public class UserServiceTesting
{
    private readonly IUserRepository _userRepository;
    private readonly ICryptographer _cryptographer;
    private readonly IMapper _mapper;
    private readonly UserService _sut;
    private readonly List<User> _context;
    private static readonly byte[] commonSalt = Encoding.ASCII.GetBytes("salt");
    private static readonly byte[] commonPassword = Encoding.ASCII.GetBytes("password");

    public UserServiceTesting()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserProfile());
        });

        _userRepository = Substitute.For<IUserRepository>();
        _cryptographer = Substitute.For<ICryptographer>();
        _mapper = Substitute.For<IMapper>();

        _sut = new UserService(_userRepository, _cryptographer, _mapper);



        _context = new List<User>
        {
            new User { Id = 1, Username = "Gamma", Password = commonPassword, Salt = commonSalt, IsActive = true },
            new User { Id = 2, Username = "Alpha", Password = commonPassword, Salt = commonSalt, IsActive = true },
            new User { Id = 3, Username = "Beta", Password = commonPassword, Salt = commonSalt, IsActive = false }
        };
    }

    [Fact]
    public void GetUsers_WhenNoQueryParameters_ReturnsAllActiveUsers()
    {
        // Arrange
        _userRepository.GetUsers()
            .Returns(_context);
        _mapper.Map<IEnumerable<ReadUserReferencelessDTO>>(_context)
            .Returns(new List<ReadUserReferencelessDTO>
            {
                new ReadUserReferencelessDTO { Id = 1, Username = "Gamma"},
                new ReadUserReferencelessDTO { Id = 2, Username = "Alpha"}
            });

        // Act
        var result = _sut.GetUsers();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetUsers_WhenSortedByName_ReturnsAllActiveUsersSortedByName()
    {
        // Arrange
        var context = new List<User>
            {
                new User { Id = 1, Username = "Gamma", Password = commonPassword, Salt = commonSalt, IsActive = true },
                new User { Id = 2, Username = "Alpha", Password = commonPassword, Salt = commonSalt, IsActive = true }
            };

        _userRepository.GetUsers(true)
            .Returns(context);

        _mapper.Map<IEnumerable<ReadUserReferencelessDTO>>(context)
            .Returns(new List<ReadUserReferencelessDTO>
            {
                new ReadUserReferencelessDTO { Id = 2, Username = "Alpha"},
                new ReadUserReferencelessDTO { Id = 1, Username = "Gamma"}
            });

        // Act
        var result = _sut.GetUsers(true);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Alpha", result.First()!.Username);
    }

    [Fact]
    public async void GetUser_WhenCalled_ShouldReturnActiveUser()
    {
        // Arrange
        var user = _context.First();
        var cancellationToken = new CancellationToken();

        _userRepository.Get(user.Id, cancellationToken)
            .Returns(user);

        _mapper.Map<ReadUserDTO>(user)
            .Returns(new ReadUserDTO { Id = user.Id, Username = user.Username });

        // Act

        var result = await _sut.GetUser(user.Id, cancellationToken);

        // Assert
        Assert.Equal(user.Id, result!.Id);

    }

    [Fact]
    public async void GetUser_WhenUserIsDeactivated_ShouldReturnNull()
    {
        
        // Arrange
        var user = _context.Last();
        var cancellationToken = new CancellationToken();

        _userRepository.Get(user.Id, cancellationToken)
            .Returns(user);

        // Act
        var result = await _sut.GetUser(user.Id, cancellationToken);

        // Assert
        Assert.Null(result);
        
    }

    [Fact]
    public async void Insert_WhenNoExistingUser_ShouldReturnSuccess()
    {
        // Arrange
        var newUser = new CreateUserDTO
        {
            Username = "Delta",
            Password = "password4"
        };

        _userRepository.GetUsers(name: newUser.Username).Returns(new List<User>());
        _cryptographer.GenerateSalt().Returns(commonSalt);
        _cryptographer.Hash(newUser.Password, commonSalt).Returns(commonPassword);
        
        // Act
        var result = await _sut.InsertUser(newUser, cancellationToken: new CancellationToken());

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Insert_WhenExistingUser_ShouldReturnBadRequest()
    {
        // Arrange
        var newUser = new CreateUserDTO
        {
            Username = "Delta",
            Password = "password4"
        };

        _userRepository.GetUsers(name: newUser.Username).Returns(new List<User>
            {
            new User { Id = 4, Username = "Delta", Password = commonPassword, Salt = commonSalt, IsActive = true }
            });

        // Act
        var result = await _sut.InsertUser(newUser, cancellationToken: new CancellationToken());

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("There's already an user with the username \"Delta\".", result.Errors.First().Message);
    }

    [Fact]
    public async void Update_WhenIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var user = _context.First();
        var updateUser = new UpdateUserDTO
        {
            Username = "Delta",
            Password = "password"
        };

        _userRepository.Get(user.Id, new CancellationToken())
            .Returns(user);

        _cryptographer.GenerateSalt().Returns(commonSalt);
        _cryptographer.Hash(updateUser.Password, commonSalt).Returns(commonPassword);

        // Act
        var result = await _sut.UpdateUser(user.Id, updateUser, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Update_WhenIdIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        var user = _context.First();
        var updateUser = new UpdateUserDTO
        {
            Username = "Delta",
            Password = "password"
        };

        _userRepository.Get(user.Id, new CancellationToken())
            .Returns((User?)null);

        // Act
        var result = await _sut.UpdateUser(user.Id, updateUser, new CancellationToken());

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("The user with Id 1 doesn't exist.", result.Errors.First().Message);
    }

    [Fact]
    public async void Update_WhenUsernameIsAlreadyTaken_ShouldReturnBadRequest()
    {
        // Arrange
        var user = _context.First();
        var updateUser = new UpdateUserDTO
        {
            Username = "Alpha",
            Password = "password"
        };

        _userRepository.Get(user.Id, new CancellationToken())
            .Returns(user);

        _userRepository.GetUsers(name: updateUser.Username).Returns(new List<User>
            {
            _context[1]
            });

        // Act
        var result = await _sut.UpdateUser(user.Id, updateUser, new CancellationToken());

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("There's already an user with the username \"Alpha\".", result.Errors.First().Message);
    }

    [Fact]
    public async void Delete_WhenIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var user = _context.First();

        _userRepository.Get(user.Id, new CancellationToken())
            .Returns(user);

        // Act
        var result = await _sut.RemoveUser(user.Id, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Delete_WhenIdIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        var user = _context.First();

        _userRepository.Get(user.Id, new CancellationToken())
            .Returns((User?)null);

        // Act
        var result = await _sut.RemoveUser(user.Id, new CancellationToken());

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("The user with Id 1 doesn't exist.", result.Errors.First().Message);
    }

    [Fact]
    public async void ToggleUserActivation_WhenIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var user = _context.First();

        _userRepository.Get(user.Id, new CancellationToken())
            .Returns(user);

        // Act
        var result = await _sut.ToggleUserActivation(user.Id, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void ToggleUserActivation_WhenIdIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        var user = _context.First();

        _userRepository.Get(user.Id, new CancellationToken())
            .Returns((User?)null);

        // Act
        var result = await _sut.ToggleUserActivation(user.Id, new CancellationToken());

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("The user with Id 1 doesn't exist.", result.Errors.First().Message);
    }

}
using Domain.DTOs.AuthenticationDTOs;
using Domain.Models;
using Domain.Utils.Cryptography;
using NSubstitute;
using Repository.Repositories.Abstract;
using Service.Services;
using System.Text;

namespace Tests.Services
{
    public class AuthenticationServiceTesting
    {

        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ICryptographer _cryptographer;
        private readonly AuthenticationService _sut;

        public AuthenticationServiceTesting()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _adminRepository = Substitute.For<IAdminRepository>();
            _cryptographer = Substitute.For<ICryptographer>();
            _sut = new AuthenticationService(_userRepository, _adminRepository, _cryptographer);
        }

        [Fact]
        public async void Test_Authenticate_Success()
        {
            // Arrange
            var loginDto = new LoginDTO
            {
                Username = "user1",
                Password = "password1"
            };
            var userSalt = Encoding.ASCII.GetBytes("salt");
            var user = new User
            {
                Id = 1,
                Username = "user1",
                Password = _cryptographer.Hash("password1", userSalt),
                Salt = userSalt,
                IsActive = true
            };
            _userRepository.GetByUserName(loginDto.Username, Arg.Any<CancellationToken>())
            .Returns(user);
            _cryptographer.Verify(loginDto.Password, user.Password, user.Salt)
                .Returns(true);
            _userRepository.GetByUserName(loginDto.Username, Arg.Any<CancellationToken>())
                .Returns(user);

            // Act
            var result = await _sut.Authenticate(loginDto, "user", CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.Contains("Authentication successful", result.Value);
        }

        [Fact]
        public async void Test_Authenticate_InvalidCredentials()
        {
            // Arrange
            var loginDto = new LoginDTO
            {
                Username = "user1",
                Password = "invalidpassword"
            };
            var userSalt = Encoding.ASCII.GetBytes("salt");
            var user = new User
            {
                Id = 1,
                Username = "user1",
                Password = _cryptographer.Hash("correctpassword", userSalt),
                Salt = userSalt,
                IsActive = true
            };
            _userRepository.GetByUserName(loginDto.Username, Arg.Any<CancellationToken>())
            .Returns(user);
            _cryptographer.Verify(loginDto.Password, user.Password, user.Salt)
                .Returns(false);

            // Act
            var result = await _sut.Authenticate(loginDto, "user", CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal("The credentials are invalid, the account is deactivated or the account doesn't exists", result.Errors[0].Message);
        }
    }

}

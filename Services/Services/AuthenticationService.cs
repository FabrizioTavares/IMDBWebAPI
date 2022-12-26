using Domain.DTOs.AuthenticationDTOs;
using Domain.Models;
using Domain.Models.Abstract;
using Domain.Utils.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Services
{
    public class AuthenticationService : IAuthenticableEntityService
    {

        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ICryptographer _cryptographer;

        public AuthenticationService(IUserRepository userRepository, IAdminRepository adminRepository, ICryptographer cryptographer)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _cryptographer = cryptographer;
        }

        public async Task<string> Authenticate<T>(LoginDTO credentials, CancellationToken cancellationToken) where T : AuthenticableClient
        {
            T? client = default;
            
            if (typeof(T) == typeof(User))
            {
                client = await _userRepository.GetUserByUserName(credentials.Username, cancellationToken) as T;
            }
            else if (typeof(T) == typeof(Admin))
            {
                client = await _adminRepository.GetAdminByUserName(credentials.Username, cancellationToken) as T;
            }

            if (client == null || !_cryptographer.Verify(credentials.Password, client.Password, client.Salt))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            return GenerateToken(client);

        }

        public string GenerateToken(AuthenticableClient user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.ImdbApiSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.GetType().Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // TODO: print token info in a better way

            return tokenHandler.WriteToken(token) + ";" + user.Username + ";" + user.GetType().Name;
        }

    }
}

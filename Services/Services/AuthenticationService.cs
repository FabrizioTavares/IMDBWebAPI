using Domain.DTOs.AuthenticationDTOs;
using Domain.Models.Abstract;
using Domain.Utils.Cryptography;
using FluentResults;
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

        public async Task<Result<string>> Authenticate(LoginDTO credentials, string role, CancellationToken cancellationToken)
        {
            AuthenticableClient? client = default!;
            
            role = role[0].ToString().ToUpper() + role[1..].ToLower();

            if (role == "User")
            {
                client = await _userRepository.GetByUserName(credentials.Username, cancellationToken);
            }
            else if (role == "Admin")
            {
                client = await _adminRepository.GetAdminByUserName(credentials.Username, cancellationToken);
            }

            if (client == null || client.IsActive == false || !_cryptographer.Verify(credentials.Password, client.Password, client.Salt))
            {
                return Result.Fail<string>("The credentials are invalid, the account is deactivated or the account doesn't exists");
            }

            return Result.Ok(GenerateToken(client) + $" - Authentication successful for user '{client.Username}' with role '{client.GetType().Name}'.");

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
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}

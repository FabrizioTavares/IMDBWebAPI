using Domain.DTOs.AuthenticationDTOs;
using Domain.Utils.Cryptography;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;

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

        public async Task<string> Authenticate(LoginDTO credentials, CancellationToken cancellationToken)
        {
            if (credentials.LoginType != 0) // for administrator
            {
                throw new NotImplementedException();
            }
            
            var user = await _userRepository.GetUserByUserName(credentials.Username, cancellationToken);

            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            var hashedPassword = _cryptographer.Hash(credentials.Password, user.Salt);
            
            if (user.Password.Equals(hashedPassword))
            {
                throw new UnauthorizedAccessException();
            }

            return "Success";
            

        }

    }
}

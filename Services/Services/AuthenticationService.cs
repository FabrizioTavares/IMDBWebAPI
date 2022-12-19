using Domain.DTOs.UserDTOs;
using Service.Services.Abstract;

namespace Service.Services
{
    public class AuthenticationService : IAuthenticableEntityService
    {
        public Task Authenticate(CredentialsUserDTO credentials)
        {
            throw new NotImplementedException();
        }

        public Task LogOff()
        {
            throw new NotImplementedException();
        }
    }
}

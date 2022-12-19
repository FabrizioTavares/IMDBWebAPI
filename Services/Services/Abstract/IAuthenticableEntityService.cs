using Domain.DTOs.UserDTOs;

namespace Service.Services.Abstract
{
    public interface IAuthenticableEntityService
    {
        Task Authenticate(CredentialsUserDTO credentials);
        Task LogOff();
    }
}

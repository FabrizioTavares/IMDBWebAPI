using Domain.DTOs.AuthenticationDTOs;

namespace Service.Services.Abstract
{
    public interface IAuthenticableEntityService
    {
        Task<string> Authenticate(LoginDTO credentials, CancellationToken cancellationToken);
    }
}

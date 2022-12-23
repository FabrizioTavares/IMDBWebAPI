using Domain.DTOs.AuthenticationDTOs;
using Domain.Models.Abstract;

namespace Service.Services.Abstract
{
    public interface IAuthenticableEntityService
    {
        Task<string> Authenticate<T>(LoginDTO credentials, CancellationToken cancellationToken) where T : AuthenticableClient;
        string GenerateToken(AuthenticableClient authenticableClient);
    }
}

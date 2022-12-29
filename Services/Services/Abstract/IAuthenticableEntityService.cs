using Domain.DTOs.AuthenticationDTOs;
using Domain.Models.Abstract;

namespace Service.Services.Abstract
{
    public interface IAuthenticableEntityService
    {
        Task<string> Authenticate(LoginDTO credentials, string role, CancellationToken cancellationToken);
        string GenerateToken(AuthenticableClient authenticableClient);
    }
}

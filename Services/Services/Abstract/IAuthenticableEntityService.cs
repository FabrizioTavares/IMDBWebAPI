using Domain.DTOs.AuthenticationDTOs;
using Domain.Models.Abstract;
using Service.Utils.Response;

namespace Service.Services.Abstract
{
    public interface IAuthenticableEntityService
    {
        Task<Result<string>> Authenticate(LoginDTO credentials, string role, CancellationToken cancellationToken);
        string GenerateToken(AuthenticableClient authenticableClient);
    }
}

using Domain.Models.Abstract;
using FluentResults;
using Service.Abstraction.AuthenticableEntityAbstractions.AuthenticationDTOs;

namespace Service.Abstraction.AuthenticableEntityAbstractions;

public interface IAuthenticableEntityService
{
    Task<Result<string>> Authenticate(LoginDTO credentials, string role, CancellationToken cancellationToken);
    string GenerateToken(AuthenticableClient authenticableClient);
}
using Application.Utils;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.AuthenticableEntityAbstractions;
using Service.Abstraction.AuthenticableEntityAbstractions.AuthenticationDTOs;

namespace Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticableEntityService _authenticationService;

    public AuthenticationController(IAuthenticableEntityService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("{role}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Authenticate([FromBody] LoginDTO loginDTO, [FromRoute] string role, CancellationToken cancellationToken = default)
    {
        var res = await _authenticationService.Authenticate(loginDTO, role, cancellationToken);
        return Ok(ConvertResult.Convert(res));
    }
}
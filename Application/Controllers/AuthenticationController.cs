using Domain.DTOs.AuthenticationDTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;

namespace Application.Controllers
{
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
            // HACK
            if (role.ToLower() == "admin")
            {
                return Ok(await _authenticationService.Authenticate<Admin>(loginDTO, cancellationToken));
            }
            else if (role.ToLower() == "user")
            {
                return Ok(await _authenticationService.Authenticate<User>(loginDTO, cancellationToken));
            }
            else
            {
                return BadRequest("Invalid user type");
            }
        }
    }
}

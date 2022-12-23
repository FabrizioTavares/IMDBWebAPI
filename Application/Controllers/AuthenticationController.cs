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

        [HttpPost("{userType}")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO loginDTO, [FromRoute] string userType, CancellationToken cancellationToken = default)
        {
            // HACK
            if(userType.ToLower() == "admin"){
                return Ok(await _authenticationService.Authenticate<Admin>(loginDTO, cancellationToken));
            }
            else if (userType.ToLower() == "user")
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

using Domain.DTOs.AuthenticationDTOs;
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

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO loginDTO, CancellationToken cancellationToken = default)
        {
            var result = await _authenticationService.Authenticate(loginDTO, cancellationToken);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}

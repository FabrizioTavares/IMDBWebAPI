using Domain.DTOs.UserDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using System.Security.Claims;

namespace Application.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAccount([FromServices] IValidator<CreateUserDTO> validator, [FromBody] CreateUserDTO newUser, CancellationToken cancellationToken = default)
        {
            var result = validator.Validate(newUser);
            if (result.IsValid)
            {
                await _userService.InsertUser(newUser, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ReadUserReferencelessDTO), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetAccounts(
            [FromQuery] bool sortedByName = false,
            [FromQuery] string? username = null,
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null)
        {
            return Ok(_userService.GetUsers(sortedByName, username, pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReadUserDTO), 200)]
        public async Task<IActionResult> GetAccountById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetUser(id, cancellationToken);
            return Ok(user);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> UpdateAccount([FromServices] IValidator<UpdateUserDTO> validator, [FromRoute] int id, [FromBody] UpdateUserDTO updatedUser, CancellationToken cancellationToken = default)
        {
            var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var agentRole = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            if (agentRole == "Admin" || agentId == id)
            {
                var result = validator.Validate(updatedUser);
                if (result.IsValid)
                {
                    await _userService.UpdateUser(id, updatedUser, cancellationToken);
                    return NoContent();
                }
                return BadRequest(result.Errors);
            }
            return Unauthorized();
        }

        [HttpPut("{id}/activation")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ToggleAccountActivation([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var agentRole = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            if (agentRole == "Admin" || agentId == id)
            {
                await _userService.ToggleUserActivation(id, cancellationToken);
                return NoContent();
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var agentRole = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            if (agentRole == "Admin" || agentId == id)
            {
                await _userService.RemoveUser(id, cancellationToken);
                return NoContent();
            }
            return Unauthorized();
        }

    }
}
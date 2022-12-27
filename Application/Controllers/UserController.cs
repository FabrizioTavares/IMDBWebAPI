using Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using Service.Validation.User;
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
        public async Task<IActionResult> CreateAccount([FromBody] CreateUserDTO newUser, CancellationToken cancellationToken = default)
        {
            var result = new CreateUserDTOValidator().Validate(newUser);
            if (result.IsValid)
            {
                await _userService.InsertUser(newUser, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
        public IActionResult GetAccounts(
            [FromQuery] bool sortedByName = false,
            [FromQuery] string? username = null,
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null)
        {
            return Ok(_userService.GetUsers(sortedByName, username, pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetUser(id, cancellationToken);
            return Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAccount([FromRoute] int id, [FromBody] UpdateUserDTO updatedUser, CancellationToken cancellationToken = default)
        {
            var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var agentRole = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            if (agentRole == "Admin" || agentId == id)
            {
                var result = new UpdateUserDTOValidator().Validate(updatedUser);
                if (result.IsValid)
                {
                    await _userService.UpdateUser(id, updatedUser, cancellationToken);
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            return Unauthorized();
        }

        [HttpPut("{id}/activation")]
        [Authorize]
        public async Task<IActionResult> ToggleAccountActivation([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var agentRole = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            if (agentRole == "Admin" || agentId == id)
            {
                await _userService.ToggleUserActivation(id, cancellationToken);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var agentRole = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            if (agentRole == "Admin" || agentId == id)
            {
                await _userService.RemoveUser(id, cancellationToken);
                return Ok();
            }
            return Unauthorized();
        }

    }
}

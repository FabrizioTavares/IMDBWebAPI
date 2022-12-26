using Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using Service.Validation.User;

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
        public IActionResult GetAllAccounts([FromQuery] string? username, CancellationToken cancellationToken = default)
        {
            if (username == null)
            {
                return Ok(_userService.GetAllUsers());
            }
            return Ok(_userService.GetUsersByUsername(username, cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetUser(id, cancellationToken);
            return Ok(user);
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> UpdateAccount([FromRoute] int id, [FromBody] UpdateUserDTO updatedUser, CancellationToken cancellationToken = default)
        {

            // TODO: Only admins can reactivate accounts. Only logged account can modify itself. other accounts can't modify other accounts.
            // ID from token?

            var result = new UpdateUserDTOValidator().Validate(updatedUser);
            if (result.IsValid)
            {
                await _userService.UpdateUser(id, updatedUser, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id, CancellationToken cancellationToken = default)
        {

            // TODO: Only currently logged account can delete itself. ID from Token?

            await _userService.RemoveUser(id, cancellationToken);
            return Ok();
        }

    }
}

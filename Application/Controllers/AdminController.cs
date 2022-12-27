using Domain.DTOs.AdminDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using Service.Validation.Admin;
using System.Security.Claims;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdministrativeService _administrativeService;

        public AdminController(IAdministrativeService administrativeService)
        {
            _administrativeService = administrativeService;
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminDTO admin, CancellationToken cancellationToken = default)
        {

            // TODO: Optimise hierarchical check: use token instead of database query

            var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var result = new CreateAdminDTOValidator().Validate(admin);
            if (result.IsValid)
            {
                await _administrativeService.InsertAdmin(agentId, admin, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdmins()
        {
            return Ok(_administrativeService.GetAllAdmins());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var admin = await _administrativeService.GetAdmin(id, cancellationToken);
            return Ok(admin);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAdmin([FromRoute] int id, [FromBody] UpdateAdminDTO updatedAdmin, CancellationToken cancellationToken = default)
        {
            var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var result = new UpdateAdminDTOValidator().Validate(updatedAdmin);
            if (result.IsValid)
            {
                await _administrativeService.UpdateAdmin(id, agentId, updatedAdmin, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("{id}/activation")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateAdmin([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            await _administrativeService.ToggleAdminActivation(id, agentId, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdmin([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            await _administrativeService.RemoveAdmin(id, agentId, cancellationToken);
            return Ok();
        }

    }
}

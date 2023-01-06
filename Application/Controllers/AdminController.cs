using Domain.DTOs.AdminDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using System.Security.Claims;

namespace Application.Controllers;

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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> CreateAdmin([FromServices] IValidator<CreateAdminDTO> validator, [FromBody] CreateAdminDTO admin, CancellationToken cancellationToken = default)
    {
        var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        var result = validator.Validate(admin);
        if (result.IsValid)
        {
            await _administrativeService.InsertAdmin(agentId, admin, cancellationToken);
            return Ok();
        }
        return BadRequest(result.Errors);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<ReadAdminDTO>), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public IActionResult GetAdmins()
    {
        return Ok(_administrativeService.GetAllAdmins());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ReadAdminDTO), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> GetAdminById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var admin = await _administrativeService.GetAdmin(id, cancellationToken);
        return Ok(admin);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> UpdateAdmin([FromServices] IValidator<UpdateAdminDTO> validator, [FromRoute] int id, [FromBody] UpdateAdminDTO updatedAdmin, CancellationToken cancellationToken = default)
    {
        var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        var result = validator.Validate(updatedAdmin);
        if (result.IsValid)
        {
            await _administrativeService.UpdateAdmin(id, agentId, updatedAdmin, cancellationToken);
            return Ok();
        }
        return BadRequest(result.Errors);
    }

    [HttpPut("{id}/activation")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> ToggleAdminActivation([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        await _administrativeService.ToggleAdminActivation(id, agentId, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> DeleteAdmin([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var agentId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        await _administrativeService.RemoveAdmin(id, agentId, cancellationToken);
        return NoContent();
    }

}
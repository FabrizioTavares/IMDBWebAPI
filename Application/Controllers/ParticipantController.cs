using Domain.DTOs.ParticipantDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using Service.Validation.Participant;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateParticipant([FromBody] CreateParticipantDTO participant, CancellationToken cancellationToken = default)
        {
            var result = new CreateParticipantDTOValidator().Validate(participant);
            if (result.IsValid)
            {
                await _participantService.Insert(participant, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
        public IActionResult GetAllParticipants([FromQuery] string? name, CancellationToken cancellationToken = default)
        {
            if (name == null)
            {
                return Ok(_participantService.GetAll());
            }
            else
            {
                return Ok(_participantService.GetParticipantsByName(name, cancellationToken));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipantById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var participant = await _participantService.Get(id, cancellationToken);
            return Ok(participant);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateParticipant([FromRoute] int id, [FromBody] UpdateParticipantDTO updatedParticipant, CancellationToken cancellationToken = default)
        {
            // TODO: Do not directly instantiate validator
            var result = new UpdateParticipantDTOValidator().Validate(updatedParticipant);
            if (result.IsValid)
            {
                await _participantService.Update(id, updatedParticipant, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteParticipant([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            await _participantService.Remove(id, cancellationToken);
            return Ok();
        }

    }
}

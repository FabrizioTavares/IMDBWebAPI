using Application.Utils;
using Domain.DTOs.ParticipantDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;

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
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> CreateParticipant([FromServices] IValidator<CreateParticipantDTO> validator, [FromBody] CreateParticipantDTO participant, CancellationToken cancellationToken = default)
        {
            var validation = validator.Validate(participant);
            if (validation.IsValid)
            {
                var res = await _participantService.Insert(participant, cancellationToken);
                return ConvertResult.Convert(res);
                // TODO: For all Create methods on all services, return the created entity (201)
            }
            return BadRequest(validation.Errors);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReadParticipantReferencelessDTO>), 200)]
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
        [ProducesResponseType(typeof(ReadParticipantDTO), 200)]
        public async Task<IActionResult> GetParticipantById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var participant = await _participantService.Get(id, cancellationToken);
            return Ok(participant);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateParticipant([FromServices] IValidator<UpdateParticipantDTO> validator, [FromRoute] int id, [FromBody] UpdateParticipantDTO updatedParticipant, CancellationToken cancellationToken = default)
        {
            var validation = validator.Validate(updatedParticipant);
            if (validation.IsValid)
            {
                var res = await _participantService.Update(id, updatedParticipant, cancellationToken);
                return ConvertResult.Convert(res);
            }
            return BadRequest(validation.Errors);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteParticipant([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var res = await _participantService.Remove(id, cancellationToken);
            return ConvertResult.Convert(res);
        }

    }
}

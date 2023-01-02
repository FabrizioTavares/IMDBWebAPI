using Domain.DTOs.GenreDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {

        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> CreateGenre([FromServices] IValidator<CreateGenreDTO> validator, [FromBody] CreateGenreDTO genre, CancellationToken cancellationToken = default)
        {
            var result = validator.Validate(genre);
            if (result.IsValid)
            {
                await _genreService.Insert(genre, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ReadGenreReferencelessDTO), 200)]
        public IActionResult GetAllGenres([FromQuery] string? title, CancellationToken cancellationToken = default)
        {
            if (title == null)
            {
                return Ok(_genreService.GetAll());
            }
            else
            {
                return Ok(_genreService.GetGenresByTitle(title, cancellationToken));
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReadGenreDTO), 200)]
        public async Task<IActionResult> GetGenreById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var genre = await _genreService.Get(id, cancellationToken);
            return Ok(genre);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateGenre([FromServices] IValidator<UpdateGenreDTO> validator, [FromRoute] int id, [FromBody] UpdateGenreDTO updatedGenre, CancellationToken cancellationToken = default)
        {
            var result = validator.Validate(updatedGenre);
            if (result.IsValid)
            {
                await _genreService.Update(id, updatedGenre, cancellationToken);
                return NoContent();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteGenre([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            await _genreService.Remove(id, cancellationToken);
            return NoContent();
        }

    }
}

using Domain.DTOs.GenreDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using Service.Validation.Genre;

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
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDTO genre, CancellationToken cancellationToken = default)
        {
            var result = new CreateGenreDTOValidator().Validate(genre);
            if (result.IsValid)
            {
                await _genreService.Insert(genre, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
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
        public async Task<IActionResult> GetGenreById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var genre = await _genreService.Get(id, cancellationToken);
            return Ok(genre);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGenre([FromRoute] int id, [FromBody] UpdateGenreDTO updatedGenre, CancellationToken cancellationToken = default)
        {
            var result = new UpdateGenreDTOValidator().Validate(updatedGenre);
            if (result.IsValid)
            {
                await _genreService.Update(id, updatedGenre, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGenre([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            await _genreService.Remove(id, cancellationToken);
            return NoContent();
        }

    }
}

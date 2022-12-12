using Domain.DTOs.GenreDTOs;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstraction;
using Service.Validation.Genre;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {

        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDTO genre, CancellationToken cancellationToken)
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
        public IActionResult GetAllGenres([FromQuery] string? title, CancellationToken cancellationToken)
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
        public async Task<IActionResult> GetGenreById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var genre = await _genreService.Get(id, cancellationToken);
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre([FromRoute] int id, [FromBody] UpdateGenreDTO updatedGenre, CancellationToken cancellationToken)
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
        public async Task<IActionResult> DeleteGenre([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _genreService.Remove(id, cancellationToken);           
            return NoContent();
        }

    }
}

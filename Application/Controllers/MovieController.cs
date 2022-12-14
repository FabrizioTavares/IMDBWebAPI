using Domain.DTOs.MovieDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using Service.Validation.Movie;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDTO movie, CancellationToken cancellationToken = default)
        {
            var result = new CreateMovieDTOValidator().Validate(movie);
            if (result.IsValid)
            {
                await _movieService.Insert(movie, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
        public IActionResult GetAllMovies([FromQuery] string? title, CancellationToken cancellationToken = default)
        {
            if (title == null)
            {
                return Ok(_movieService.GetAll());
            }
            else
            {
                return Ok(_movieService.GetMoviesByTitle(title, cancellationToken));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var movie = await _movieService.Get(id, cancellationToken);
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieDTO updatedMovie, CancellationToken cancellationToken = default)
        {
            var result = new UpdateMovieDTOValidator().Validate(updatedMovie);
            if (result.IsValid)
            {
                await _movieService.Update(id, updatedMovie, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            await _movieService.Remove(id, cancellationToken);
            return Ok();
        }
    }
}

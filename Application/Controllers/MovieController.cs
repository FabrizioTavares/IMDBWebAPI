using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.MovieDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.DTOs.VoteDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using System.Security.Claims;

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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateMovie([FromServices] IValidator<CreateMovieDTO> validator, [FromBody] CreateMovieDTO movie, CancellationToken cancellationToken = default)
        {
            var result = validator.Validate(movie);
            if (result.IsValid)
            {
                await _movieService.Insert(movie, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ReadMovieReferencelessDTO), 200)]
        public IActionResult GetMovies(
            [FromQuery] bool sortedByTitle,
            [FromQuery] bool sortedByRating,
            [FromQuery] string? title,
            [FromQuery] string? actor,
            [FromQuery] string? director,
            [FromQuery] string? genre,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize
            )
        {
            var movies = _movieService.GetMovies(sortedByTitle, sortedByRating, title, actor, director, genre, pageNumber, pageSize);
            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        [ProducesResponseType(typeof(ReadMovieDTO), 200)]
        public async Task<IActionResult> GetMovieById([FromRoute] int movieId, CancellationToken cancellationToken = default)
        {
            var movie = await _movieService.Get(movieId, cancellationToken);
            return Ok(movie);
        }

        [HttpPut("{movieId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateMovie([FromServices] IValidator<UpdateMovieDTO> validator, [FromRoute] int movieId, [FromBody] UpdateMovieDTO updatedMovie, CancellationToken cancellationToken = default)
        {
            var result = validator.Validate(updatedMovie);
            if (result.IsValid)
            {
                await _movieService.Update(movieId, updatedMovie, cancellationToken);
                return NoContent();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("{movieId}/performances")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddPerformanceToMovie([FromServices] IValidator<CreatePerformanceDTO> validator, [FromRoute] int movieId, [FromBody] CreatePerformanceDTO newPerformance, CancellationToken cancellationToken = default)
        {
            var result = validator.Validate(newPerformance);
            if (result.IsValid)
            {
                await _movieService.AddPerformanceToMovie(movieId, newPerformance, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{movieId}/performances/{participantid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeletePerformanceFromMovie([FromRoute] int movieId, [FromRoute] int participantid, CancellationToken cancellationToken = default)
        {
            await _movieService.RemovePerformanceFromMovie(movieId, participantid, cancellationToken);
            return NoContent();
        }

        [HttpPost("{movieId}/directions")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddDirectionToMovie([FromServices] IValidator<CreateDirectionDTO> validator, [FromRoute] int movieId, [FromBody] CreateDirectionDTO newDirection, CancellationToken cancellationToken = default)
        {
            var result = validator.Validate(newDirection);
            if (result.IsValid)
            {
                await _movieService.AddDirectionToMovie(movieId, newDirection, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{movieId}/directions/{participantid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteDirectionFromMovie([FromRoute] int movieId, [FromRoute] int participantid, CancellationToken cancellationToken = default)
        {
            await _movieService.RemoveDirectionFromMovie(movieId, participantid, cancellationToken);
            return NoContent();
        }

        [HttpPost("{movieId}/genres/{genreid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddGenreToMovie([FromRoute] int movieId, [FromRoute] int genreid, CancellationToken cancellationToken = default)
        {
            await _movieService.AddGenreToMovie(movieId, genreid, cancellationToken);
            return Ok();
        }

        [HttpDelete("{movieId}/genres/{genreid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteGenreFromMovie([FromRoute] int movieId, [FromRoute] int genreid, CancellationToken cancellationToken = default)
        {
            await _movieService.RemoveGenreFromMovie(movieId, genreid, cancellationToken);
            return NoContent();
        }

        [HttpPost("{movieId}/reviews")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddReviewToMovie([FromServices] IValidator<CreateVoteDTO> validator, [FromRoute] int movieId, [FromBody] CreateVoteDTO newReview, CancellationToken cancellationToken = default)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var result = validator.Validate(newReview);
            if (result.IsValid)
            {
                await _movieService.AddReviewToMovie(movieId, userId, newReview, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{movieId}/reviews/")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteReviewFromMovie([FromRoute] int movieId, CancellationToken cancellationToken = default)
        {
            // TODO: Create static class to retrieve token information
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            await _movieService.RemoveReviewFromMovie(movieId, userId, cancellationToken);
            return NoContent();
        }


        [HttpDelete("{movieId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteMovie([FromRoute] int movieId, CancellationToken cancellationToken = default)
        {
            await _movieService.Remove(movieId, cancellationToken);
            return NoContent();
        }
    }
}

using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.MovieDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.DTOs.VoteDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using Service.Validation.Direction;
using Service.Validation.Movie;
using Service.Validation.Performance;
using Service.Validation.Vote;
using System.Data;

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
        public async Task<IActionResult> GetMovieById([FromRoute] int movieId, CancellationToken cancellationToken = default)
        {
            var movie = await _movieService.Get(movieId, cancellationToken);
            return Ok(movie);
        }

        [HttpPut("{movieId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovie([FromRoute] int movieId, [FromBody] UpdateMovieDTO updatedMovie, CancellationToken cancellationToken = default)
        {
            var result = new UpdateMovieDTOValidator().Validate(updatedMovie);
            if (result.IsValid)
            {
                await _movieService.Update(movieId, updatedMovie, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("{movieId}/performances")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPerformanceToMovie([FromRoute] int movieId, [FromBody] CreatePerformanceDTO newPerformance, CancellationToken cancellationToken = default)
        {
            var result = new CreatePerformanceDTOValidator().Validate(newPerformance);
            if (result.IsValid)
            {
                await _movieService.AddPerformanceToMovie(movieId, newPerformance, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{movieId}/performances/{participantid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePerformanceFromMovie([FromRoute] int movieId, [FromRoute] int participantid, CancellationToken cancellationToken = default)
        {
            await _movieService.RemovePerformanceFromMovie(movieId, participantid, cancellationToken);
            return Ok();
        }

        [HttpPost("{movieId}/directions")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDirectionToMovie([FromRoute] int movieId, [FromBody] CreateDirectionDTO newDirection, CancellationToken cancellationToken = default)
        {
            var result = new CreateDirectionDTOValidator().Validate(newDirection);
            if (result.IsValid)
            {
                await _movieService.AddDirectionToMovie(movieId, newDirection, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{movieId}/directions/{participantid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDirectionFromMovie([FromRoute] int movieId, [FromRoute] int participantid, CancellationToken cancellationToken = default)
        {
            await _movieService.RemoveDirectionFromMovie(movieId, participantid, cancellationToken);
            return Ok();
        }

        [HttpPost("{movieId}/genres/{genreid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGenreToMovie([FromRoute] int movieId, [FromRoute] int genreid, CancellationToken cancellationToken = default)
        {
            await _movieService.AddGenreToMovie(movieId, genreid, cancellationToken);
            return Ok();
        }
        
        [HttpDelete("{movieId}/genres/{genreid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGenreFromMovie([FromRoute] int movieId, [FromRoute] int genreid, CancellationToken cancellationToken = default)
        {
            await _movieService.RemoveGenreFromMovie(movieId, genreid, cancellationToken);
            return Ok();
        }

        [HttpPost("{movieId}/reviews")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddReviewToMovie([FromRoute] int movieId, [FromBody] CreateVoteDTO newReview, CancellationToken cancellationToken = default)
        {
            var result = new CreateVoteDTOValidator().Validate(newReview);
            if (result.IsValid)
            {
                await _movieService.AddReviewToMovie(movieId, newReview, cancellationToken);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{movieId}/reviews/{userId}")]
        [Authorize(Roles = "User")] //TODO: Retrieve ID from token
        public async Task<IActionResult> DeleteReviewFromMovie([FromRoute] int movieId, [FromRoute] int userId, CancellationToken cancellationToken = default)
        {
            await _movieService.RemoveReviewFromMovie(movieId, userId, cancellationToken);
            return Ok();
        }


        [HttpDelete("{movieId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int movieId, CancellationToken cancellationToken = default)
        {
            await _movieService.Remove(movieId, cancellationToken);
            return Ok();
        }
    }
}

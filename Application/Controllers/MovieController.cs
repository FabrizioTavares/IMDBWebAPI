using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.MovieDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.DTOs.VoteDTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Abstract;
using Service.Validation.Direction;
using Service.Validation.Movie;
using Service.Validation.Performance;
using Service.Validation.Vote;

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
                var value = _movieService.GetMoviesByTitle(title, cancellationToken);
                return Ok(value);
            }
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovieById([FromRoute] int movieId, CancellationToken cancellationToken = default)
        {
            var movie = await _movieService.Get(movieId, cancellationToken);
            return Ok(movie);
        }

        [HttpPut("{movieId}")]
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
        public async Task<IActionResult> DeletePerformanceFromMovie([FromRoute] int movieId, [FromRoute] int participantid, CancellationToken cancellationToken = default)
        {
            await _movieService.RemovePerformanceFromMovie(movieId, participantid, cancellationToken);
            return Ok();
        }

        [HttpPost("{movieId}/directions")]
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
        public async Task<IActionResult> DeleteDirectionFromMovie([FromRoute] int movieId, [FromRoute] int participantid, CancellationToken cancellationToken = default)
        {
            await _movieService.RemoveDirectionFromMovie(movieId, participantid, cancellationToken);
            return Ok();
        }

        [HttpPost("{movieId}/genres/{genreid}")]
        public async Task<IActionResult> AddGenreToMovie([FromRoute] int movieId, [FromRoute] int genreid, CancellationToken cancellationToken = default)
        {
            await _movieService.AddGenreToMovie(movieId, genreid, cancellationToken);
            return Ok();
        }
        
        [HttpDelete("{movieId}/genres/{genreid}")]
        public async Task<IActionResult> DeleteGenreFromMovie([FromRoute] int movieId, [FromRoute] int genreid, CancellationToken cancellationToken = default)
        {
            await _movieService.RemoveGenreFromMovie(movieId, genreid, cancellationToken);
            return Ok();
        }

        [HttpPost("{movieId}/reviews")]
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
        public async Task<IActionResult> DeleteReviewFromMovie([FromRoute] int movieId, [FromRoute] int userId, CancellationToken cancellationToken = default)
        {
            await _movieService.RemoveReviewFromMovie(movieId, userId, cancellationToken);
            return Ok();
        }


        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int movieId, CancellationToken cancellationToken = default)
        {
            await _movieService.Remove(movieId, cancellationToken);
            return Ok();
        }
    }
}

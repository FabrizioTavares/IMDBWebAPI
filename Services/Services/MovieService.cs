using AutoMapper;
using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.MovieDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.DTOs.VoteDTOs;
using Domain.Models;
using FluentResults;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;
using Service.Utils.Responses;

namespace Service.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;
        private readonly IDirectionRepository _directionRepository;
        private readonly IPerformanceRepository _performanceRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVoteRepository _voteRepository;

        private readonly IMapper _mapper;

        public MovieService(
            IMovieRepository movieRepository,
            IDirectionRepository directionRepository,
            IPerformanceRepository performanceRepository,
            IParticipantRepository participantRepository,
            IGenreRepository genreRepository,
            IUserRepository userRepository,
            IVoteRepository voteRepository,
            IMapper mapper)
        {
            _movieRepository = movieRepository;
            _directionRepository = directionRepository;
            _performanceRepository = performanceRepository;
            _participantRepository = participantRepository;
            _genreRepository = genreRepository;
            _userRepository = userRepository;
            _voteRepository = voteRepository;
            _mapper = mapper;
        }

        public async Task<ReadMovieDTO?> Get(int id, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.Get(id, cancellationToken);
            return _mapper.Map<ReadMovieDTO>(movie);
        }

        public IEnumerable<ReadMovieReferencelessDTO?> GetMovies(
            bool sortedByTitle = false,
            bool sortedByRating = false,
            string? title = null,
            string? actor = null,
            string? director = null,
            string? genre = null,
            int? pageNumber = null,
            int? pageSize = null
            )
        {
            var movies = _movieRepository.GetMovies(
                sortedByTitle,
                sortedByRating,
                title,
                actor,
                director,
                genre,
                pageNumber,
                pageSize
                );
            return _mapper.Map<IEnumerable<ReadMovieReferencelessDTO>>(movies);
        }

        public async Task<Result> Insert(CreateMovieDTO movie, CancellationToken cancellationToken)
        {
            var newMovie = _mapper.Map<Movie>(movie);
            await _movieRepository.Insert(newMovie, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> AddPerformanceToMovie(int movieId, CreatePerformanceDTO dto, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.Get(movieId, cancellationToken);
            var participant = await _participantRepository.Get(dto.ParticipantId, cancellationToken);

            if (movie == null || participant == null)
            {
                return Result.Fail(new BadRequestError("Invalid movie or participant ID"));
            }

            var performance = _mapper.Map<Performance>(dto);

            performance.Participant = participant;
            movie.Cast.Add(performance);
            await _movieRepository.Update(movie, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> RemovePerformanceFromMovie(int movieId, int participantId, CancellationToken cancellationToken)
        {
            var performanceToBeRemoved = await _performanceRepository.GetComposite(movieId, participantId, cancellationToken);

            if (performanceToBeRemoved == null)
            {
                return Result.Fail(new BadRequestError("Invalid movie or participant ID"));
            }

            await _performanceRepository.Remove(performanceToBeRemoved, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> AddDirectionToMovie(int movieId, CreateDirectionDTO newDirection, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.Get(movieId, cancellationToken);
            var director = await _participantRepository.Get(newDirection.ParticipantId, cancellationToken);

            if (movie == null || director == null)
            {
                return Result.Fail(new BadRequestError("Invalid movie or participant ID"));
            }

            var mappedDirection = _mapper.Map<Direction>(newDirection);

            mappedDirection.Participant = director;
            movie.Direction.Add(mappedDirection);
            await _movieRepository.Update(movie, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> RemoveDirectionFromMovie(int movieId, int participantId, CancellationToken cancellationToken)
        {
            var directionToBeRemoved = await _directionRepository.GetComposite(movieId, participantId, cancellationToken);

            if (directionToBeRemoved == null)
            {
                return Result.Fail(new BadRequestError("Invalid movie or participant ID"));
            }
            await _directionRepository.Remove(directionToBeRemoved, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> AddGenreToMovie(int movieId, int genreId, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.Get(movieId, cancellationToken);
            var genre = await _genreRepository.Get(genreId, cancellationToken);

            if (movie == null || genre == null)
            {
                return Result.Fail(new BadRequestError("Invalid movie or genre ID"));
            }

            movie.Genres.Add(genre);
            await _movieRepository.Update(movie, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> RemoveGenreFromMovie(int movieId, int genreId, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.Get(movieId, cancellationToken);
            var genre = await _genreRepository.Get(genreId, cancellationToken);

            if (movie == null || genre == null)
            {
                return Result.Fail(new BadRequestError("Invalid movie or genre ID"));
            }

            movie.Genres.Remove(genre);
            await _movieRepository.Update(movie, cancellationToken);
            return Result.Ok();
        }


        public async Task<Result> Update(int id, UpdateMovieDTO updatedMovie, CancellationToken cancellationToken)
        {
            var movieToBeUpdated = await _movieRepository.Get(id, cancellationToken);
            if (movieToBeUpdated == null)
            {
                return Result.Fail(new BadRequestError("Invalid movie ID"));
            }
            var map = _mapper.Map(updatedMovie, movieToBeUpdated);
            await _movieRepository.Update(map, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> Remove(int id, CancellationToken cancellationToken)
        {
            var movieToBeDeleted = await _movieRepository.Get(id, cancellationToken);
            if (movieToBeDeleted == null)
            {
                return Result.Fail(new BadRequestError("Invalid movie ID"));
            }
            else
            {
                await _movieRepository.Remove(movieToBeDeleted, cancellationToken);
                return Result.Ok();
            }
        }

        public async Task<Result> AddReviewToMovie(int movieId, int userId, CreateVoteDTO newReview, CancellationToken cancellationToken)
        {
            var movieToBeReviewed = await _movieRepository.Get(movieId, cancellationToken);
            var user = await _userRepository.Get(userId, cancellationToken);

            if (movieToBeReviewed == null || user == null)
            {
                return Result.Fail(new BadRequestError("Invalid movie or user ID"));
            }

            var mappedReview = _mapper.Map<Vote>(newReview);

            mappedReview.User = user;
            mappedReview.Movie = movieToBeReviewed;
            mappedReview.MovieId = movieId;

            await _voteRepository.Insert(mappedReview, cancellationToken);
            user.Votes.Add(mappedReview);
            movieToBeReviewed.AddVote(mappedReview);
            await _movieRepository.Update(movieToBeReviewed, cancellationToken);
            await _userRepository.Update(user, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> RemoveReviewFromMovie(int movieId, int userId, CancellationToken cancellationToken)
        {
            var reviewToBeRemoved = await _voteRepository.GetComposite(movieId, userId, cancellationToken);

            if (reviewToBeRemoved == null)
            {
                return Result.Fail(new BadRequestError("Invalid movie or user ID"));
            }

            var movieReviewed = reviewToBeRemoved.Movie;
            var reviewer = reviewToBeRemoved.User;

            reviewToBeRemoved.Movie.RemoveVote(reviewToBeRemoved);
            await _voteRepository.Remove(reviewToBeRemoved, cancellationToken);
            await _movieRepository.Update(movieReviewed, cancellationToken);
            await _userRepository.Update(reviewer, cancellationToken);
            return Result.Ok();
        }
    }
}

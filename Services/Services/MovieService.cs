using AutoMapper;
using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.MovieDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.Models;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;

namespace Service.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;
        private readonly IDirectionRepository _directionRepository;
        private readonly IPerformanceRepository _performanceRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IGenreRepository _genreRepository;

        private readonly IMapper _mapper;

        public MovieService(
            IMovieRepository movieRepository,
            IDirectionRepository directionRepository,
            IPerformanceRepository performanceRepository,
            IParticipantRepository participantRepository,
            IGenreRepository genreRepository,
            IMapper mapper)
        {
            _movieRepository = movieRepository;
            _directionRepository = directionRepository;
            _performanceRepository = performanceRepository;
            _participantRepository = participantRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ReadMovieDTO?> Get(int id, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.Get(id, cancellationToken);
            return _mapper.Map<ReadMovieDTO>(movie);
        }

        public IEnumerable<ReadMovieReferencelessDTO?> GetAll()
        {
            var movies = _movieRepository.GetAll();
            return _mapper.Map<IEnumerable<ReadMovieReferencelessDTO>>(movies);
        }

        public IEnumerable<ReadMovieReferencelessDTO?> GetMoviesByTitle(string title, CancellationToken cancellationToken)
        {
            var movies = _movieRepository.GetMoviesByTitle(title, cancellationToken);
            return _mapper.Map<IEnumerable<ReadMovieReferencelessDTO>>(movies);
        }

        public async Task Insert(CreateMovieDTO movie, CancellationToken cancellationToken)
        {
            var newMovie = _mapper.Map<Movie>(movie);
            await _movieRepository.Insert(newMovie, cancellationToken);
        }

        public async Task AddPerformanceToMovie(int movieId, CreatePerformanceDTO newPerformance, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.Get(movieId, cancellationToken);
            var participant = await _participantRepository.Get(newPerformance.ParticipantId, cancellationToken);

            if (movie == null || participant == null)
            {
                throw new ApplicationException("Invalid movie or participant ID");
            }

            var mappedPerformance = _mapper.Map<Performance>(newPerformance);
            
            mappedPerformance.Movie = movie;
            mappedPerformance.MovieId = movieId;
            mappedPerformance.Participant = participant;
            mappedPerformance.ParticipantId = participant.Id;

            await _performanceRepository.Insert(mappedPerformance, cancellationToken);
            movie.Cast.Add(mappedPerformance);
            await _movieRepository.Update(movie, cancellationToken);
        }

        public async Task RemovePerformanceFromMovie(int movieId, int participantId, CancellationToken cancellationToken)
        {
            var performanceToBeRemoved = await _performanceRepository.GetComposite(movieId, participantId, cancellationToken);

            if (performanceToBeRemoved == null)
            {
                throw new ApplicationException("Invalid movie or participant ID");
            }

            await _performanceRepository.Remove(performanceToBeRemoved, cancellationToken);
        }

        public async Task AddDirectionToMovie(int movieId, CreateDirectionDTO newDirection, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.Get(movieId, cancellationToken);
            var director = await _participantRepository.Get(newDirection.ParticipantId, cancellationToken);

            if (movie == null || director == null)
            {
                throw new ApplicationException("Invalid movie or director ID");
            }

            var mappedDirection = _mapper.Map<Direction>(newDirection);

            mappedDirection.Movie = movie;
            mappedDirection.MovieId = movieId;
            mappedDirection.Participant = director;
            mappedDirection.ParticipantId = director.Id;

            await _directionRepository.Insert(mappedDirection, cancellationToken);
            movie.Direction.Add(mappedDirection);
            await _movieRepository.Update(movie, cancellationToken);
        }

        public async Task RemoveDirectionFromMovie(int movieId, int participantId, CancellationToken cancellationToken)
        {
            var directionToBeRemoved = await _directionRepository.GetComposite(movieId, participantId, cancellationToken);

            if (directionToBeRemoved == null)
            {
                throw new ApplicationException("Invalid movie or participant ID");
            }
            await _directionRepository.Remove(directionToBeRemoved, cancellationToken);
        }

        public async Task AddGenreToMovie(int movieId, int genreId, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.Get(movieId, cancellationToken);
            var genre = await _genreRepository.Get(genreId, cancellationToken);

            if (movie == null || genre == null)
            {
                throw new ApplicationException("Invalid movie or genre ID");
            }

            movie.Genres.Add(genre);
            await _movieRepository.Update(movie, cancellationToken);
        }


        public async Task Update(int id, UpdateMovieDTO updatedMovie, CancellationToken cancellationToken)
        {
            var movieToBeUpdated = await _movieRepository.Get(id, cancellationToken);
            if (movieToBeUpdated == null)
            {
                throw new ApplicationException("Movie not found");
            }
            var map = _mapper.Map(updatedMovie, movieToBeUpdated);
            await _movieRepository.Update(map, cancellationToken);
        }

        public async Task Remove(int id, CancellationToken cancellationToken)
        {
            var movieToBeDeleted = await _movieRepository.Get(id, cancellationToken);
            if (movieToBeDeleted == null)
            {
                throw new ApplicationException("Movie not found");
            }
            else
            {
                await _movieRepository.Remove(movieToBeDeleted, cancellationToken);
            }
        }
    }
}

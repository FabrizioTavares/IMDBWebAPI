using AutoMapper;
using Domain.DTOs.MovieDTOs;
using Domain.Models;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;

namespace Service.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<ReadMovieDTO?> Get(int id, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.Get(id, cancellationToken);
            return _mapper.Map<ReadMovieDTO>(movie);
        }

        public IEnumerable<ReadMovieDTO?> GetAll()
        {
            var movies = _movieRepository.GetAll();
            return _mapper.Map<IEnumerable<ReadMovieDTO>>(movies);
        }

        public IEnumerable<ReadMovieDTO?> GetMoviesByTitle(string title, CancellationToken cancellationToken)
        {
            var movies = _movieRepository.GetMoviesByTitle(title, cancellationToken);
            return _mapper.Map<IEnumerable<ReadMovieDTO>>(movies);
        }

        public async Task Insert(CreateMovieDTO movie, CancellationToken cancellationToken)
        {
            var newMovie = _mapper.Map<Movie>(movie);
            await _movieRepository.Insert(newMovie, cancellationToken);
        }

        public async Task Update(int id, UpdateMovieDTO updatedMovie, CancellationToken cancellationToken)
        {
            var movieToBeUpdated = await _movieRepository.Get(id, cancellationToken);
            if (movieToBeUpdated == null)
            {
                throw new ApplicationException("Movie not found");
            }
            else
            {
                var map = _mapper.Map<Movie>(updatedMovie);
                await _movieRepository.Update(map, cancellationToken);
            }
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

using AutoMapper;
using Domain.DTOs.GenreDTOs;
using Domain.Models;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;

namespace Service.Services
{
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;

        public GenreService(IMapper mapper, IGenreRepository genreRepository)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        public async Task<ReadGenreDTO?> Get(int id, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.Get(id, cancellationToken);
            return _mapper.Map<ReadGenreDTO>(genre);
        }

        public IEnumerable<ReadGenreDTO> GetGenresByTitle(string title, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ReadGenreDTO>>(_genreRepository.GetGenresByTitle(title));
        }

        public IEnumerable<ReadGenreDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<ReadGenreDTO>>(_genreRepository.GetAll());
        }

        public Task Insert(CreateGenreDTO genre, CancellationToken cancellationToken)
        {
            var existingGenre = _genreRepository.GetGenresByTitle(genre.Title);
            if (existingGenre.Any())
            {
                throw new ApplicationException("Genre already exists");
            }
            else
            {
                return _genreRepository.Insert(_mapper.Map<Genre>(genre), cancellationToken);
            }
        }

        public async Task Remove(int id, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.Get(id, cancellationToken);
            if (genre == null)
            {
                throw new ApplicationException("Genre not found");
            }
            else
            {
                await _genreRepository.Remove(genre, cancellationToken);
            }
        }

        public async Task Update(int id, UpdateGenreDTO updatedGenre, CancellationToken cancellationToken)
        {
            var genreToBeUpdated = await _genreRepository.Get(id, cancellationToken);
            if (genreToBeUpdated == null)
            {
                throw new ApplicationException("Genre not found");
            }
            else
            {
                var map = _mapper.Map(updatedGenre, genreToBeUpdated);
                await _genreRepository.Update(map, cancellationToken);
            }
        }
    }
}

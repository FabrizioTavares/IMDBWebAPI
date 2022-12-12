using AutoMapper;
using Domain.DTOs.GenreDTOs;
using Domain.Models;
using Repository.Repositories.Abstract;
using Service.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<ReadGenreDTO?> GetAll()
        {
            return _mapper.Map<IEnumerable<ReadGenreDTO>>(_genreRepository.GetAll());
        }

        public Task Insert(CreateGenreDTO genre, CancellationToken cancellationToken)
        {
            var existingGenre = _genreRepository.GetGenresByTitle(genre.Title);
            if (existingGenre.Any())
            {
                throw new Exception("Genre already exists");
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
                throw new Exception("Genre not found");
            }
            await _genreRepository.Remove(genre, cancellationToken);
        }

        public Task Update(int id, UpdateGenreDTO updatedGenre, CancellationToken cancellationToken)
        {
            var genreToBeUpdated = _genreRepository.Get(id, cancellationToken).Result;
            if (genreToBeUpdated == null)
            {
                throw new Exception("Genre not found");
            }
            return _genreRepository.Update(_mapper.Map(updatedGenre, genreToBeUpdated), cancellationToken);
        }
    }
}

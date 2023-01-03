using AutoMapper;
using Domain.DTOs.GenreDTOs;
using Domain.Models;
using FluentResults;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;
using Service.Utils.Responses;

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

        public IEnumerable<ReadGenreReferencelessDTO> GetGenresByTitle(string title, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ReadGenreReferencelessDTO>>(_genreRepository.GetGenresByTitle(title));
        }

        public IEnumerable<ReadGenreReferencelessDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<ReadGenreReferencelessDTO>>(_genreRepository.GetAll());
        }

        public async Task<Result<Genre>> Insert(CreateGenreDTO genre, CancellationToken cancellationToken)
        {
            var existingGenre = _genreRepository.GetGenresByTitle(genre.Title);
            if (existingGenre.Any())
            {
                return Result.Fail(new BadRequestError($"The genre with title {genre.Title} already exists.")); 
            }
            else
            {
                var newGenre = await _genreRepository.Insert(_mapper.Map<Genre>(genre), cancellationToken);
                return Result.Ok(newGenre);
            }
        }

        public async Task<Result> Remove(int id, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.Get(id, cancellationToken);
            if (genre == null)
            {
                return Result.Fail(new NotFoundError($"The genre with id {id} does not exist."));
            }
            else
            {
                await _genreRepository.Remove(genre, cancellationToken);
                return Result.Ok();
            }
        }

        public async Task<Result> Update(int id, UpdateGenreDTO updatedGenre, CancellationToken cancellationToken)
        {
            var genreToBeUpdated = await _genreRepository.Get(id, cancellationToken);
            if (genreToBeUpdated == null)
            {
                return Result.Fail(new NotFoundError($"The genre with id {id} does not exist."));
            }
            else
            {
                var map = _mapper.Map(updatedGenre, genreToBeUpdated);
                await _genreRepository.Update(map, cancellationToken);
                return Result.Ok();
            }
        }
    }
}

using Domain.DTOs.GenreDTOs;
using FluentResults;

namespace Service.Services.Abstract
{
    public interface IGenreService
    {
        IEnumerable<ReadGenreReferencelessDTO?> GetAll();
        Task<ReadGenreDTO?> Get(int id, CancellationToken cancellationToken);
        IEnumerable<ReadGenreReferencelessDTO?> GetGenresByTitle(string title, CancellationToken cancellationToken);
        Task<Result> Insert(CreateGenreDTO genre, CancellationToken cancellationToken);
        Task<Result> Update(int id, UpdateGenreDTO genre, CancellationToken cancellationToken);
        Task<Result> Remove(int id, CancellationToken cancellationToken);
    }
}

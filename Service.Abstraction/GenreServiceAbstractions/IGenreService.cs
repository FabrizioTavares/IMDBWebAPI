using Domain.Models;
using FluentResults;
using Service.Abstraction.GenreServiceAbstractions.GenreDTOs;

namespace Service.Abstraction.GenreServiceAbstractions;

public interface IGenreService
{
    IEnumerable<ReadGenreReferencelessDTO?> GetAll();
    Task<ReadGenreDTO?> Get(int id, CancellationToken cancellationToken);
    IEnumerable<ReadGenreReferencelessDTO?> GetGenresByTitle(string title, CancellationToken cancellationToken);
    Task<Result<Genre>> Insert(CreateGenreDTO genre, CancellationToken cancellationToken);
    Task<Result> Update(int id, UpdateGenreDTO genre, CancellationToken cancellationToken);
    Task<Result> Remove(int id, CancellationToken cancellationToken);
}
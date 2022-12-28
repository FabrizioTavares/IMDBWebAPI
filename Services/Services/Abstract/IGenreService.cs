using Domain.DTOs.GenreDTOs;

namespace Service.Services.Abstract
{
    public interface IGenreService
    {
        IEnumerable<ReadGenreReferencelessDTO?> GetAll();
        Task<ReadGenreDTO?> Get(int id, CancellationToken cancellationToken);
        IEnumerable<ReadGenreReferencelessDTO?> GetGenresByTitle(string title, CancellationToken cancellationToken);
        Task Insert(CreateGenreDTO genre, CancellationToken cancellationToken);
        Task Update(int id, UpdateGenreDTO genre, CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
    }
}

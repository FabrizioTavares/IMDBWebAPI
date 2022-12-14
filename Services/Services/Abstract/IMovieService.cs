using Domain.DTOs.MovieDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Abstract
{
    public interface IMovieService
    {
        Task<ReadMovieDTO?> Get(int id, CancellationToken cancellationToken);
        IEnumerable<ReadMovieDTO?> GetAll();
        IEnumerable<ReadMovieDTO?> GetMoviesByTitle(string title, CancellationToken cancellationToken);
        Task Insert(CreateMovieDTO movie, CancellationToken cancellationToken);
        Task Update(int id, UpdateMovieDTO movie, CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
    }
}

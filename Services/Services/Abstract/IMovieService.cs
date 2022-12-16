using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.MovieDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.Models;
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
        IEnumerable<ReadMovieReferencelessDTO?> GetAll();
        IEnumerable<ReadMovieReferencelessDTO?> GetMoviesByTitle(string title, CancellationToken cancellationToken);
        Task Insert(CreateMovieDTO movie, CancellationToken cancellationToken);
        Task AddPerformanceToMovie(int movieId, CreatePerformanceDTO newPerformance, CancellationToken cancellationToken);
        Task RemovePerformanceFromMovie(int movieId, int participantId, CancellationToken cancellationToken);
        Task AddDirectionToMovie(int movieId, CreateDirectionDTO newDirection, CancellationToken cancellationToken);
        Task RemoveDirectionFromMovie(int movieId, int participantId, CancellationToken cancellationToken);
        Task AddGenreToMovie(int movieId, int genreId, CancellationToken cancellationToken);
        Task Update(int id, UpdateMovieDTO movie, CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
    }
}

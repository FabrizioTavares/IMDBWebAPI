using Domain.DTOs.PerformanceDTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Abstract
{
    public interface IPerformanceService
    {
        Task<ReadPerformanceDTO?> Get(int movieId, int participantId, CancellationToken cancellationToken);
        IEnumerable<ReadPerformanceDTO?> GetByCharacterName(string characterName, CancellationToken cancellationToken);
        IEnumerable<ReadPerformanceDTO?> GetByMovieId(int movieId, CancellationToken cancellationToken);
        IEnumerable<ReadPerformanceDTO?> GetByParticipantId(int participantId, CancellationToken cancellationToken);
        IEnumerable<ReadPerformanceDTO?> GetAll();
        Task Insert(int movieId, int participantId, CreatePerformanceDTO performanceDTO, CancellationToken cancellationToken);
        Task Update(int movieId, int participantId, UpdatePerformanceDTO performanceDTO, CancellationToken cancellationToken);
        Task Remove(int movieId, int participantId, CancellationToken cancellationToken);
    }
}

using Domain.DTOs.DirectionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Abstract
{
    public interface IDirectionService
    {
        Task<ReadDirectionDTO?> Get(int movieId, int participantId, CancellationToken cancellationToken);
        IEnumerable<ReadDirectionDTO?> GetDirectionsByMovie(int movieId, CancellationToken cancellationToken);
        IEnumerable<ReadDirectionDTO?> GetDirectionsByParticipant(int participantId, CancellationToken cancellationToken);
        IEnumerable<ReadDirectionDTO?> GetAll();
        Task Insert(CreateDirectionDTO direction, CancellationToken cancellationToken);
        Task Remove(int movieId, int participantId, CancellationToken cancellationToken);
    }
}

using Domain.DTOs.ParticipantDTOs;
using Domain.Models;
using Service.Utils.Response;

namespace Service.Services.Abstract
{
    public interface IParticipantService
    {
        IEnumerable<ReadParticipantReferencelessDTO?> GetAll();
        Task<ReadParticipantDTO?> Get(int id, CancellationToken cancellationToken);
        IEnumerable<ReadParticipantReferencelessDTO?> GetParticipantsByName(string firstName, CancellationToken cancellationToken);
        Task<Result<Participant>> Insert(CreateParticipantDTO participant, CancellationToken cancellationToken);
        Task Update(int id, UpdateParticipantDTO participant, CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);

    }
}

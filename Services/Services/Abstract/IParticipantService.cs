using Domain.DTOs.ParticipantDTOs;

namespace Service.Services.Abstract
{
    public interface IParticipantService
    {
        IEnumerable<ReadParticipantDTO?> GetAll();
        Task<ReadParticipantDTO?> Get(int id, CancellationToken cancellationToken);
        IEnumerable<ReadParticipantDTO?> GetParticipantsByName(string firstName, CancellationToken cancellationToken);
        Task Insert(CreateParticipantDTO participant, CancellationToken cancellationToken);
        Task Update(int id, UpdateParticipantDTO participant, CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);

    }
}

using FluentResults;
using Service.Abstraction.ParticipantServiceAbstractions.ParticipantDTOs;

namespace Service.Abstraction.ParticipantServiceAbstractions;

public interface IParticipantService
{
    IEnumerable<ReadParticipantReferencelessDTO?> GetAll();
    Task<ReadParticipantDTO?> Get(int id, CancellationToken cancellationToken);
    IEnumerable<ReadParticipantReferencelessDTO?> GetParticipantsByName(string firstName, CancellationToken cancellationToken);
    Task<Result<int>> Insert(CreateParticipantDTO participant, CancellationToken cancellationToken);
    Task<Result> Update(int id, UpdateParticipantDTO participant, CancellationToken cancellationToken);
    Task<Result> Remove(int id, CancellationToken cancellationToken);

}
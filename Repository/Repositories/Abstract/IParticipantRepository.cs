using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IParticipantRepository : IBaseRepository<Participant>
    {
        Task<Participant?> GetParticipantByName(string name, CancellationToken cancellationToken);
    }

}

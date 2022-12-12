using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IParticipantRepository : IBaseRepository<Participant>
    {
        IEnumerable<Participant> GetParticipantsByName(string name);
    }

}

using Domain.Models;

namespace Repository.Abstraction;

public interface IParticipantRepository : IBaseRepository<Participant>
{
    IEnumerable<Participant> GetParticipantsByName(string name);
}
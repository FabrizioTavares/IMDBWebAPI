using AutoMapper;
using Domain.DTOs.ParticipantDTOs;
using Domain.Models;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;

namespace Service.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IMapper _mapper;
        private readonly IParticipantRepository _participantRepository;

        public ParticipantService(IMapper mapper, IParticipantRepository participantRepository)
        {
            _mapper = mapper;
            _participantRepository = participantRepository;
        }

        public async Task<ReadParticipantDTO?> Get(int id, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.Get(id, cancellationToken);
            return _mapper.Map<ReadParticipantDTO>(participant);
        }

        public IEnumerable<ReadParticipantReferencelessDTO> GetParticipantsByName(string name, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ReadParticipantReferencelessDTO>>(_participantRepository.GetParticipantsByName(name));
        }

        public IEnumerable<ReadParticipantReferencelessDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<ReadParticipantReferencelessDTO>>(_participantRepository.GetAll());
        }

        public Task Insert(CreateParticipantDTO participant, CancellationToken cancellationToken)
        {
            // TODO: Possible improvement: instead of inserting a single participant, insert a list of participants. do this for all inserts.
            // This would allow for a more efficient way of inserting data into the database.
            var existingParticipant = _participantRepository.GetParticipantsByName(participant.Name);
            if (existingParticipant.Any())
            {
                throw new ApplicationException(("Participant already exists"));
            }
            else
            {
                return _participantRepository.Insert(_mapper.Map<Participant>(participant), cancellationToken);
            }
        }

        public async Task Remove(int id, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.Get(id, cancellationToken);
            if (participant == null)
            {
                throw new ApplicationException("Participant not found");
            }
            else
            {
                await _participantRepository.Remove(participant, cancellationToken);
            }
        }

        public async Task Update(int id, UpdateParticipantDTO updatedParticipant, CancellationToken cancellationToken)
        {
            var participantToBeUpdated = await _participantRepository.Get(id, cancellationToken);
            if (participantToBeUpdated == null)
            {
                throw new ApplicationException("Participant not found");
            }
            else
            {
                var map = _mapper.Map(updatedParticipant, participantToBeUpdated);
                await _participantRepository.Update(map, cancellationToken);
            }
        }
    }
}

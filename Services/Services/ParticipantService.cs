using AutoMapper;
using Domain.DTOs.ParticipantDTOs;
using Domain.Models;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<ReadParticipantDTO> GetParticipantsByName(string name, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ReadParticipantDTO>>(_participantRepository.GetParticipantsByName(name));
        }

        public IEnumerable<ReadParticipantDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<ReadParticipantDTO>>(_participantRepository.GetAll());
        }

        public Task Insert(CreateParticipantDTO participant, CancellationToken cancellationToken)
        {
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

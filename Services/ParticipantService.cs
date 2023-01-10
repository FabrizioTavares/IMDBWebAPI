using AutoMapper;
using Domain.Models;
using FluentResults;
using Repository.Abstraction;
using Service.Abstraction.ParticipantServiceAbstractions;
using Service.Abstraction.ParticipantServiceAbstractions.ParticipantDTOs;
using Service.Utils.Responses;

namespace Service;

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

    public async Task<Result<int>> Insert(CreateParticipantDTO participant, CancellationToken cancellationToken)
    {
        var existingParticipant = _participantRepository.GetParticipantsByName(participant.Name);
        if (existingParticipant.Any())
        {
            return Result.Fail(new BadRequestError("Participant already exists"));
        }
        else
        {
            var mappedParticipant = _mapper.Map<Participant>(participant);
            var createdParticipant = await _participantRepository.Insert(mappedParticipant, cancellationToken);

            return Result.Ok(createdParticipant.Id);
        }
    }

    public async Task<Result> Remove(int id, CancellationToken cancellationToken)
    {
        var participant = await _participantRepository.Get(id, cancellationToken);
        if (participant == null)
        {
            return Result.Fail(new NotFoundError("Participant not found"));
        }
        else
        {
            await _participantRepository.Remove(participant, cancellationToken);
            return Result.Ok();
        }
    }

    public async Task<Result> Update(int id, UpdateParticipantDTO updatedParticipant, CancellationToken cancellationToken)
    {
        var participantToBeUpdated = await _participantRepository.Get(id, cancellationToken);
        if (participantToBeUpdated == null)
        {
            return Result.Fail(new NotFoundError("Participant not found"));
        }
        else
        {
            var map = _mapper.Map(updatedParticipant, participantToBeUpdated);
            await _participantRepository.Update(map, cancellationToken);
            return Result.Ok();
        }
    }
}
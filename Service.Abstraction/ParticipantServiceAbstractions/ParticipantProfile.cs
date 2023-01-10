using AutoMapper;
using Domain.Models;
using Service.Abstraction.ParticipantServiceAbstractions.ParticipantDTOs;

namespace Service.Abstraction.ParticipantServiceAbstractions;

public class ParticipantProfile : Profile
{
    public ParticipantProfile()
    {
        CreateMap<CreateParticipantDTO, Participant>();
        CreateMap<Participant, ReadParticipantDTO>()
            .ForMember(p => p.MoviesDirected, opt => opt
            .MapFrom(p => p.MoviesDirected
            .Select(d => d.Movie)));
        CreateMap<Participant, ReadParticipantReferencelessDTO>();
        CreateMap<UpdateParticipantDTO, Participant>()
            .ForAllMembers(m => m
            .Condition((src, dest, srcMember) => srcMember != default));
    }
}
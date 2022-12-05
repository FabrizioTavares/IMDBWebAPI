using AutoMapper;
using Domain.DTOs.ParticipantDTOs;
using Domain.Models;

namespace Domain.AutomapperProfiles
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<CreateParticipantDTO, Participant>();
            CreateMap<Participant, ReadParticipantDTO>();
            CreateMap<UpdateParticipantDTO, Participant>();
        }
    }
}

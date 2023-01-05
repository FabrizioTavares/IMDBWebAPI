using AutoMapper;
using Domain.DTOs.PerformanceDTOs;
using Domain.Models;

namespace Domain.AutomapperProfiles;

public class PerformanceProfile : Profile
{
    public PerformanceProfile()
    {
        CreateMap<CreatePerformanceDTO, Performance>();
        CreateMap<Performance, ReadPerformanceFromMovieDTO>()
            .ForMember(dest => dest.Actor, opt => opt
            .MapFrom(src => src.Participant));
        CreateMap<Performance, ReadPerformanceFromParticipantDTO>();
        CreateMap<UpdatePerformanceDTO, Performance>();
    }
}
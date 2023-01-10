using AutoMapper;
using Domain.Models;
using Service.Abstraction.MovieServiceAbstractions.PerformanceDTOs;

namespace Service.Abstraction.MovieServiceAbstractions;

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
using AutoMapper;
using Domain.DTOs.PerformanceDTOs;
using Domain.Models;

namespace Domain.AutomapperProfiles
{
    public class PerformanceProfile : Profile
    {
        public PerformanceProfile()
        {
            CreateMap<CreatePerformanceDTO, Performance>();
            CreateMap<Performance, ReadPerformanceDTO>();
            CreateMap<UpdatePerformanceDTO, Performance>();
        }
    }
}

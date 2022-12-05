using AutoMapper;
using Domain.DTOs.DirectionDTOs;
using Domain.Models;

namespace Domain.AutomapperProfiles
{
    public class DirectionProfile : Profile
    {
        public DirectionProfile()
        {
            CreateMap<CreateDirectionDTO, Direction>();
            CreateMap<Direction, ReadDirectionDTO>();
            CreateMap<UpdateDirectionDTO, Direction>();
        }
    }
}

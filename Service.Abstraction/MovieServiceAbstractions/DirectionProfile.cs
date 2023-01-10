using AutoMapper;
using Domain.Models;
using Service.Abstraction.MovieServiceAbstractions.DirectionDTOs;

namespace Service.Abstraction.MovieServiceAbstractions;

public class DirectionProfile : Profile
{
    public DirectionProfile()
    {
        CreateMap<CreateDirectionDTO, Direction>();
        CreateMap<UpdateDirectionDTO, Direction>();
    }
}
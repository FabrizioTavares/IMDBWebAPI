using AutoMapper;
using Domain.DTOs.UserDTOs;
using Domain.Models;

namespace Domain.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, ReadUserReferencelessDTO>();
            CreateMap<User, ReadUserDTO>();
            CreateMap<UpdateUserDTO, User>()
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForAllMembers(m => m
                .Condition((src, dest, srcMember) => srcMember != default));
        }
    }
}

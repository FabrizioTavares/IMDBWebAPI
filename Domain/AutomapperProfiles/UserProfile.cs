using AutoMapper;
using Domain.DTOs.UserDTOs;
using Domain.Models;

namespace Domain.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CredentialsUserDTO, User>();
            CreateMap<User, ReadUserDTO>();
            CreateMap<UpdateUserDTO, User>()
                .ForAllMembers(m => m
                .Condition((src, dest, srcMember) => srcMember != default)); ;
        }
    }
}

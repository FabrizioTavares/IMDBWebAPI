using AutoMapper;
using Domain.Models;
using Service.Abstraction.UserServiceAbstractions.UserDTOs;

namespace Service.Abstraction.UserServiceAbstractions;

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
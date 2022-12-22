﻿using AutoMapper;
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
                .ForAllMembers(m => m
                .Condition((src, dest, srcMember) => srcMember != default)); ;
        }
    }
}

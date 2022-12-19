﻿using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.Models;

namespace Domain.AutomapperProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<CreateAdminDTO, Admin>();
            CreateMap<Admin, ReadAdminDTO>();
            CreateMap<UpdateAdminDTO, Admin>()
                .ForAllMembers(m => m
                .Condition((src, dest, srcMember) => srcMember != default)); ;
        }
    }
}

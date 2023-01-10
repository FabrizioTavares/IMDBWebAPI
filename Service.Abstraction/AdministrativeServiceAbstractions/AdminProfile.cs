using AutoMapper;
using Domain.Models;
using Service.Abstraction.AdministrativeServiceAbstractions.AdminDTOs;

namespace Service.Abstraction.AdministrativeServiceAbstractions;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<CreateAdminDTO, Admin>();
        CreateMap<Admin, ReadAdminDTO>();
        CreateMap<UpdateAdminDTO, Admin>()
            .ForMember(a => a.Password, opt => opt.Ignore())
            .ForAllMembers(m => m
            .Condition((src, dest, srcMember) => srcMember != default)); ;
    }
}
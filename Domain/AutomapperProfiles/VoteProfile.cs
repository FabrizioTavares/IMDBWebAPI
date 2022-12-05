using AutoMapper;
using Domain.DTOs.VoteDTOs;
using Domain.Models;

namespace Domain.AutomapperProfiles
{
    public class VoteProfile : Profile
    {
        public VoteProfile()
        {
            CreateMap<CreateVoteDTO, Vote>();
            CreateMap<Vote, ReadVoteDTO>();
            CreateMap<UpdateVoteDTO, Vote>();
        }
    }
}

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
            CreateMap<Vote, ReadVoteWithoutMovieDTO>()
                .ForMember(dest => dest.Voter, opt => opt
                .MapFrom(src => src.User));
            CreateMap<Vote, ReadVoteWithoutVoterDTO>();
            CreateMap<UpdateVoteDTO, Vote>();
        }
    }
}
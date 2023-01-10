using AutoMapper;
using Domain.Models;
using Service.Abstraction.MovieServiceAbstractions.VoteDTOs;

namespace Service.Abstraction.MovieServiceAbstractions;

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
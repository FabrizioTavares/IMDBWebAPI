using AutoMapper;
using Domain.DTOs.MovieDTOs;
using Domain.Models;

namespace Domain.AutomapperProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<CreateMovieDTO, Movie>();
            CreateMap<Movie, ReadMovieDTO>()
                .ForMember(m => m.Direction, opt => opt
                .MapFrom(m => m.Direction
                .Select(d => d.Participant)));
            CreateMap<Movie, ReadMovieReferencelessDTO>();
            CreateMap<UpdateMovieDTO, Movie>()
                .ForAllMembers(m => m
                .Condition((src, dest, srcMember) => srcMember != default));
        }
    }
}

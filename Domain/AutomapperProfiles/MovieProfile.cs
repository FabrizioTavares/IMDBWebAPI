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
            CreateMap<Movie, ReadMovieDTO>();
            CreateMap<UpdateMovieDTO, Movie>();
        }
    }
}

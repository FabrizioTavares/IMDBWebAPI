using AutoMapper;
using Domain.DTOs.GenreDTOs;
using Domain.Models;

namespace Domain.AutomapperProfiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<CreateGenreDTO, Genre>();
        CreateMap<Genre, ReadGenreDTO>();
        CreateMap<Genre, ReadGenreReferencelessDTO>();
        CreateMap<UpdateGenreDTO, Genre>();
    }
}
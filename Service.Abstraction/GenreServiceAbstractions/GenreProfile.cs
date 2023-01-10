using AutoMapper;
using Domain.Models;
using Service.Abstraction.GenreServiceAbstractions.GenreDTOs;

namespace Service.Abstraction.GenreServiceAbstractions;

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
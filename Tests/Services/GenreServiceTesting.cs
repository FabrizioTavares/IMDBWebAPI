using Domain.DTOs.GenreDTOs;
using Domain.Models;
using Moq;
using Service.Services.Abstract;
using Xunit;
using AutoMapper;
using Repository.Repositories.Abstract;
using Service.Services;
using Domain.AutomapperProfiles;
using Domain.DTOs.PerformanceDTOs;

namespace Tests.Services;

public class GenreServiceTesting
{

    private readonly IMapper _mapper;
    private readonly Mock<IGenreRepository> _mockGenreRepository = new();
    private readonly List<Genre>? context;

    public GenreServiceTesting()
    {
        
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new GenreProfile());
        });
        _mapper = config.CreateMapper();

        
        context = new List<Genre>
        {
            new Genre { Id = 1, Title = "Action" },
            new Genre { Id = 2, Title = "Adventure" },
            new Genre { Id = 3, Title = "Drama" }
        };

        //_mockGenreRepository.Setup(repo => repo.GetAll()).Returns(context);
        //_mockGenreRepository.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns((int i, CancellationToken cancellationToken) => context.FirstOrDefault(x => x.Id == i));
        //_mockGenreRepository.Setup(repo => repo.Insert(It.IsAny<Genre>(), It.IsAny<CancellationToken>())).Returns((Genre genre, CancellationToken cancellationToken) => Task.FromResult(genre));

        
    }



}

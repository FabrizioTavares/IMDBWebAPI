using AutoMapper;
using Service.Abstraction.GenreServiceAbstractions;
using Service.Abstraction.GenreServiceAbstractions.GenreDTOs;

namespace Tests.Services;

public class GenreServiceTesting
{

    private readonly IMapper _mapper;
    private readonly IGenreRepository _genreRepository;
    private readonly List<Genre> context;
    private readonly GenreService _sut;

    public GenreServiceTesting()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new GenreProfile());
        });

        _genreRepository = Substitute.For<IGenreRepository>();
        _mapper = Substitute.For<IMapper>();
        _sut = new GenreService(_mapper, _genreRepository);

        context = new List<Genre>
        {
            new Genre { Id = 1, Title = "Action" },
            new Genre { Id = 2, Title = "Adventure" },
            new Genre { Id = 3, Title = "Drama" }
        };

    }

    [Fact]
    public void GetAll_WhenCalled_ShouldReturnAllGenres()
    {
        // Arrange
        _genreRepository.GetAll().Returns(context);
        _mapper.Map<IEnumerable<ReadGenreReferencelessDTO>>(context).Returns(new List<ReadGenreReferencelessDTO>
        {
            new ReadGenreReferencelessDTO { Id = 1, Title = "Action" },
            new ReadGenreReferencelessDTO { Id = 2, Title = "Adventure" },
            new ReadGenreReferencelessDTO { Id = 3, Title = "Drama" }
        });

        // Act
        var genres = _sut.GetAll();

        // Assert
        Assert.Equal(3, genres.Count());
    }

    [Fact]
    public void GetGenresByTitle_WhenTitleContainsQuery_ShouldReturnGenre()
    {
        // Arrange
        var _context = new List<Genre>
        {
            new Genre { Id = 1, Title = "Action" },
        };

        _genreRepository.GetGenresByTitle("Action").Returns(_context);
        _mapper.Map<IEnumerable<ReadGenreReferencelessDTO>>(_context).Returns(new List<ReadGenreReferencelessDTO>
        {
            new ReadGenreReferencelessDTO { Id = 1, Title = "Action" }
        });

        // Act
        var result = _sut.GetGenresByTitle("Action", CancellationToken.None);

        // Assert
        Assert.Equal(1, result.Count());
        Assert.Equal("Action", result.First().Title);
    }

    [Fact]
    public async void GetById_WhenIdIsValid_ShouldReturnGenre()
    {
        // Arrange
        _genreRepository.Get(2, CancellationToken.None).Returns(context[1]);
        _mapper.Map<ReadGenreDTO>(context[1]).Returns(new ReadGenreDTO { Id = 2, Title = "Adventure" });

        // Act
        var result = await _sut.Get(2, CancellationToken.None);

        // Assert
        Assert.Equal("Adventure", result?.Title);
    }

    [Fact]
    public async void Insert_WhenGenreAlreadyExists_ShouldReturnBadRequestError()
    {
        // Arrange
        _genreRepository.GetGenresByTitle("Action").Returns(new List<Genre> { new Genre { Title = "Action" } });
        _mapper.Map<Genre>(Arg.Any<CreateGenreDTO>()).Returns(new Genre());

        var createGenreDTO = new CreateGenreDTO { Title = "Action" };
        var cancellationToken = new CancellationToken();

        // Act
        var result = await _sut.Insert(createGenreDTO, cancellationToken);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("The genre with title Action already exists.", result.Errors.First().Message);
    }

    [Fact]
    public async void Insert_WhenGenreDoesNotExist_ShouldReturnNewGenre()
    {
        // Arrange
        _genreRepository.GetGenresByTitle("Action").Returns(new List<Genre>());
        _genreRepository.Insert(Arg.Any<Genre>(), Arg.Any<CancellationToken>()).Returns(new Genre { Title = "Action" });
        _mapper.Map<Genre>(Arg.Any<CreateGenreDTO>()).Returns(new Genre());

        var createGenreDTO = new CreateGenreDTO { Title = "Action" };
        var cancellationToken = new CancellationToken();

        // Act
        var result = await _sut.Insert(createGenreDTO, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.IsType<Genre>(result.Value);
        Assert.Equal("Action", result.Value.Title);
    }

    [Fact]
    public async void Remove_ShouldReturnNotFoundError_WhenGenreDoesNotExist()
    {
        // Arrange
        _genreRepository.Get(1, Arg.Any<CancellationToken>()).Returns((Genre?)null);

        var cancellationToken = new CancellationToken();

        // Act
        var result = await _sut.Remove(1, cancellationToken);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<NotFoundError>(result.Errors.First());
        Assert.Equal("The genre with id 1 does not exist.", result.Errors.First().Message);
    }

    [Fact]
    public async void Remove_ShouldReturnSuccess_WhenGenreExists()
    {
        // Arrange
        _genreRepository.Get(1, Arg.Any<CancellationToken>()).Returns(new Genre { Id = 1, Title = "Action" });

        var cancellationToken = new CancellationToken();

        // Act
        var result = await _sut.Remove(1, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Update_ShouldReturnSuccess_OnUpdateSuccessful()
    {
        // Arrange
        _genreRepository.Get(1, Arg.Any<CancellationToken>()).Returns(new Genre { Id = 1, Title = "Action" });
        _mapper.Map<Genre>(Arg.Any<UpdateGenreDTO>()).Returns(new Genre { Id = 1, Title = "Adventure" });

        var updateGenreDTO = new UpdateGenreDTO { Title = "Adventure" };
        var cancellationToken = new CancellationToken();

        // Act
        var result = await _sut.Update(1, updateGenreDTO, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Update_ShouldReturnNotFoundError_WhenGenreDoesNotExist()
    {
        // Arrange
        _genreRepository.Get(1, Arg.Any<CancellationToken>()).Returns((Genre?)null);

        var updateGenreDTO = new UpdateGenreDTO { Title = "Adventure" };
        var cancellationToken = new CancellationToken();

        // Act
        var result = await _sut.Update(1, updateGenreDTO, cancellationToken);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<NotFoundError>(result.Errors.First());
        Assert.Equal("The genre with id 1 does not exist.", result.Errors.First().Message);
    }

}
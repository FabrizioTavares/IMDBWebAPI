using AutoMapper;
using Service.Abstraction.GenreServiceAbstractions;
using Service.Abstraction.MovieServiceAbstractions;
using Service.Abstraction.MovieServiceAbstractions.DirectionDTOs;
using Service.Abstraction.MovieServiceAbstractions.MovieDTOs;
using Service.Abstraction.MovieServiceAbstractions.PerformanceDTOs;
using Service.Abstraction.MovieServiceAbstractions.VoteDTOs;
using Service.Abstraction.ParticipantServiceAbstractions;

namespace Tests.Services;

public class MovieServiceTesting
{
    private readonly IMovieRepository _movieRepository;
    private readonly IDirectionRepository _directionRepository;
    private readonly IPerformanceRepository _performanceRepository;
    private readonly IParticipantRepository _participantRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IUserRepository _userRepository;
    private readonly IVoteRepository _voteRepository;
    private readonly IMapper _mapper;
    private readonly IMapper _trueMapper;

    private List<Movie> _moviesContext;
    private List<Participant> _participantsContext;

    private readonly MovieService _sut;

    public MovieServiceTesting()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MovieProfile());
            cfg.AddProfile(new DirectionProfile());
            cfg.AddProfile(new PerformanceProfile());
            cfg.AddProfile(new ParticipantProfile());
            cfg.AddProfile(new GenreProfile());
            cfg.AddProfile(new VoteProfile());
        });

        _movieRepository = Substitute.For<IMovieRepository>();
        _directionRepository = Substitute.For<IDirectionRepository>();
        _performanceRepository = Substitute.For<IPerformanceRepository>();
        _participantRepository = Substitute.For<IParticipantRepository>();
        _genreRepository = Substitute.For<IGenreRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _voteRepository = Substitute.For<IVoteRepository>();
        _mapper = Substitute.For<IMapper>();

        _trueMapper = config.CreateMapper();

        _sut = new MovieService(
            _movieRepository,
            _directionRepository,
            _performanceRepository,
            _participantRepository,
            _genreRepository,
            _userRepository,
            _voteRepository,
            _mapper);

        _participantsContext = new List<Participant>
        {
            new Participant { Id = 1, Name = "Gamma", Biography = "Participant Number 1" },
            new Participant { Id = 2, Name = "Alpha", Biography = "Participant Number 2" },
            new Participant { Id = 3, Name = "Beta", Biography = "Participant Number 3" },
            new Participant { Id = 4, Name = "Delta", Biography = "Participant Number 4" },
        };


        _moviesContext = new List<Movie>
        {
            new Movie{
                Id = 1,
                Title = "Gamma Movie",
                Synopsis = "Description of Gamma Movie",
                ReleaseYear = 2022,
                Duration = 120,
                Genres = new List<Genre>
                {
                    new Genre { Id = 1, Title = "Action" },
                },
                Cast = new List<Performance>
                {
                    new Performance {
                        MovieId = 1,
                        ParticipantId = _participantsContext[0].Id,
                        Participant = _participantsContext[0],
                        CharacterName = "Gamma Movie Protagonist"},
                },
                Direction = new List<Direction>
                {
                    new Direction {
                        MovieId = 1,
                        ParticipantId = 2,
                        Participant = _participantsContext[1],
                    },
                },
                Votes = new List<Vote>()
            },
            new Movie{
                Id = 2,
                Title = "Alpha Movie",
                Synopsis = "Description of Alpha Movie",
                ReleaseYear = 2021,
                Duration = 120,
                Genres = new List<Genre>
                {
                    new Genre { Id = 2, Title = "Comedy" },
                },
                Cast = new List<Performance>
                {
                    new Performance {
                        MovieId = 2,
                        ParticipantId = 2,
                        Participant = _participantsContext[1],
                        CharacterName = "Alpha Movie Protagonist"
                    },
                },
                Direction = new List<Direction>
                {
                    new Direction {
                        MovieId = 2,
                        ParticipantId = 2,
                        Participant = _participantsContext[1],
                    },
                },
                Votes = new List<Vote>()
            },
            new Movie{
                Id = 3,
                Title = "Beta Movie",
                Synopsis = "Description of Beta Movie",
                ReleaseYear = 2020,
                Duration = 120,
                Genres = new List<Genre>
                {
                    new Genre { Id = 3, Title = "Drama" },
                },
                Cast = new List<Performance>
                {
                    new Performance {
                        MovieId = 3,
                        ParticipantId = 3,
                        Participant = _participantsContext[2],
                        CharacterName = "Beta Movie Protagonist"},
                },
                Direction = new List<Direction>
                {
                    new Direction {
                        MovieId = 3,
                        ParticipantId = 4,
                        Participant = _participantsContext[3],
                    },
                },
                Votes = new List<Vote>()
            }
        };

        _moviesContext[0].AddVote(new Vote { MovieId = _moviesContext[0].Id, UserId = 1, Rating = 1 });
        _moviesContext[0].AddVote(new Vote { MovieId = _moviesContext[0].Id, UserId = 2, Rating = 3 });
        _moviesContext[0].AddVote(new Vote { MovieId = _moviesContext[0].Id, UserId = 3, Rating = 3 });

        _moviesContext[1].AddVote(new Vote { MovieId = _moviesContext[1].Id, UserId = 1, Rating = 4 });
        _moviesContext[1].AddVote(new Vote { MovieId = _moviesContext[1].Id, UserId = 2, Rating = 4 });
        _moviesContext[1].AddVote(new Vote { MovieId = _moviesContext[1].Id, UserId = 3, Rating = 4 });

    }

    [Fact]
    public async void Get_WhenCalled_ReturnMovie()
    {
        // Arrange

        var movie = _moviesContext[0];
        var movieDto = _trueMapper.Map<ReadMovieDTO>(movie);

        _movieRepository.Get(movie.Id, default)
            .Returns(movie);
        _mapper.Map<ReadMovieDTO>(movie)
            .Returns(movieDto);

        // Act
        var result = await _sut.Get(1, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Gamma Movie", result.Title);
        Assert.Equal("Description of Gamma Movie", result.Synopsis);
        Assert.Equal(2022, result.ReleaseYear);
        Assert.Equal(120, result.Duration);
        Assert.Equal(1, result.Genres!.Count());
        Assert.Equal(1, result.Cast!.Count());
        Assert.Equal(1, result.Direction!.Count());
        Assert.Equal(3, result.Votes!.Count());
    }

    [Fact]
    public async void Get_WhenNoResult_ReturnNull()
    {
        // Arrange
        _movieRepository.Get(1, default)
            .Returns((Movie?)null);

        // Act
        var result = await _sut.Get(1, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetMovies_WhenNoQuery_ReturnAllMovies()
    {
        // Arrange

        var _contextDTO = _trueMapper.Map<IEnumerable<ReadMovieReferencelessDTO>>(_moviesContext);

        _movieRepository.GetMovies(false, false)
            .Returns(_moviesContext);
        _mapper.Map<IEnumerable<ReadMovieReferencelessDTO>>(_moviesContext)
            .Returns(_contextDTO);

        // Act
        var result = _sut.GetMovies(false, false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void GetMovies_WhenSortedByTitle_ReturnsMoviesSortedByTitle()
    {
        // Arrange

        var sortedMoviesContext = _moviesContext.OrderBy(m => m.Title).ToList();
        var sortedMoviesContextDTO = _trueMapper.Map<IEnumerable<ReadMovieReferencelessDTO>>(sortedMoviesContext);

        _movieRepository.GetMovies(true, false)
            .Returns(sortedMoviesContext);
        _mapper.Map<IEnumerable<ReadMovieReferencelessDTO>>(sortedMoviesContext)
            .Returns(sortedMoviesContextDTO);

        // Act
        var result = _sut.GetMovies(true, false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
        Assert.Equal("Alpha Movie", result.First()!.Title);
        Assert.Equal("Beta Movie", result.ElementAt(1)!.Title);
        Assert.Equal("Gamma Movie", result.Last()!.Title);
    }

    [Fact]
    public void GetMovies_WhenSortedByRating_ReturnsMoviesSortedByRating()
    {

        // Arrange

        var sortedMoviesContext = _moviesContext.OrderByDescending(m => m.Rating).ToList();
        var sortedMoviesContextDTO = _trueMapper.Map<IEnumerable<ReadMovieReferencelessDTO>>(sortedMoviesContext);

        _movieRepository.GetMovies(false, true)
            .Returns(sortedMoviesContext);
        _mapper.Map<IEnumerable<ReadMovieReferencelessDTO>>(sortedMoviesContext)
            .Returns(sortedMoviesContextDTO);

        // Act
        var result = _sut.GetMovies(false, true);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
        Assert.Equal("Alpha Movie", result.First()!.Title);
        Assert.Equal("Gamma Movie", result.ElementAt(1)!.Title);
        Assert.Equal("Beta Movie", result.Last()!.Title);

    }

    [Fact]
    public async void Insert_WhenCalled_ShouldReturnSuccess()
    {
        // Arrange
        var newMovie = new CreateMovieDTO
        {
            Title = "Delta Movie",
            Synopsis = "Description of Delta Movie",
            ReleaseYear = 2019,
            Duration = 80,
        };

        _mapper.Map<Movie>(newMovie)
            .Returns(_trueMapper.Map<Movie>(newMovie));

        // Act
        var result = await _sut.Insert(newMovie, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void AddPerformanceToMovie_WhenParticipantIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var performance = new CreatePerformanceDTO
        {
            CharacterName = "Villain",
            ParticipantId = 3,
        };

        _movieRepository.Get(1, default)
            .Returns(_moviesContext[0]);
        _participantRepository.Get(performance.ParticipantId, default)
            .Returns(_participantsContext[2]);
        _mapper.Map<Performance>(performance)
            .Returns(_trueMapper.Map<Performance>(performance));

        // Act
        var result = await _sut.AddPerformanceToMovie(1, performance, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void AddPerformanceToMovie_WhenParticipantIdIsInvalid_ShouldReturnBadRequestError()
    {
        // Arrange
        var performance = new CreatePerformanceDTO
        {
            CharacterName = "Villain",
            ParticipantId = 4,
        };

        _movieRepository.Get(1, default)
            .Returns(_moviesContext[0]);
        _participantRepository.Get(performance.ParticipantId, default)
            .Returns((Participant?)null);

        // Act
        var result = await _sut.AddPerformanceToMovie(1, performance, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("Invalid movie or participant ID", result.Errors.First().Message);
    }

    [Fact]
    public async void RemovePerformanceFromMovie_WhenParticipantIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var movie = _moviesContext[0];
        var performance = _moviesContext[0].Cast.First();


        _performanceRepository.GetComposite(movie.Id, performance.ParticipantId, default)
            .Returns(performance);

        // Act
        var result = await _sut.RemovePerformanceFromMovie(movie.Id, performance.ParticipantId, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void RemovePerformanceFromMovie_WhenParticipantIdIsInvalid_ShouldReturnBadRequestError()
    {

        // Arrange
        var movie = _moviesContext[0];
        var performance = _moviesContext[0].Cast.First();

        _performanceRepository.GetComposite(movie.Id, performance.ParticipantId, default)
            .Returns((Performance?)null);

        // Act
        var result = await _sut.RemovePerformanceFromMovie(movie.Id, performance.ParticipantId, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("Invalid movie or participant ID", result.Errors.First().Message);

    }

    [Fact]
    public async void AddDirectionToMovie_WhenParticipantIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var direction = new CreateDirectionDTO
        {
            ParticipantId = 3,
        };

        _movieRepository.Get(1, default)
            .Returns(_moviesContext[0]);
        _participantRepository.Get(direction.ParticipantId, default)
            .Returns(_participantsContext[2]);
        _mapper.Map<Direction>(direction)
            .Returns(_trueMapper.Map<Direction>(direction));

        // Act
        var result = await _sut.AddDirectionToMovie(1, direction, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void AddDirectionToMovie_WhenParticipantIdIsInvalid_ShouldReturnFailure()
    {
        // Arrange
        var direction = new CreateDirectionDTO
        {
            ParticipantId = 4,
        };

        _movieRepository.Get(1, default)
            .Returns(_moviesContext[0]);
        _participantRepository.Get(direction.ParticipantId, default)
            .Returns((Participant?)null);

        // Act
        var result = await _sut.AddDirectionToMovie(1, direction, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact]
    public async void RemoveDirectionFromMovie_WhenParticipantIdisValid_ShouldReturnSuccess()
    {
        // Arrange
        var movie = _moviesContext[0];
        var direction = _moviesContext[0].Direction.First();

        _directionRepository.GetComposite(movie.Id, direction.ParticipantId, default)
            .Returns(direction);

        // Act
        var result = await _sut.RemoveDirectionFromMovie(movie.Id, direction.ParticipantId, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void RemoveDirectionFromMovie_WhenParticipantIdIsInvalid_ShouldReturnBadRequestError()
    {
        // Arrange
        var movie = _moviesContext[0];

        _directionRepository.GetComposite(movie.Id, 4, default)
            .Returns((Direction?)null);

        // Act
        var result = await _sut.RemoveDirectionFromMovie(movie.Id, 4, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("Invalid movie or participant ID", result.Errors.First().Message);
    }

    [Fact]
    public async void AddGenreToMovie_WhenGenreIdIsValid_ShouldReturnSuccess()
    {
        // Arrange

        var movie = _moviesContext[0];
        var genre = new Genre
        {
            Id = 4,
            Title = "Terror",
        };

        _movieRepository.Get(movie.Id, default)
            .Returns(movie);
        _genreRepository.Get(genre.Id, default)
            .Returns(genre);
        _mapper.Map<Genre>(genre)
            .Returns(_trueMapper.Map<Genre>(genre));

        // Act
        var result = await _sut.AddGenreToMovie(movie.Id, genre.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void AddGenreToMovie_WhenGenreIdIsInvalid_ShouldReturnBadRequestError()
    {
        // Arrange
        var movie = _moviesContext[0];

        _movieRepository.Get(movie.Id, default)
            .Returns(movie);
        _genreRepository.Get(4, default)
            .Returns((Genre?)null);

        // Act
        var result = await _sut.AddGenreToMovie(movie.Id, 4, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("Invalid movie or genre ID", result.Errors.First().Message);
    }

    [Fact]
    public async void RemoveGenreFromMovie_WhenGenreIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var movie = _moviesContext[0];
        var genre = _moviesContext[0].Genres.First();

        _movieRepository.Get(movie.Id, default)
            .Returns(movie);
        _genreRepository.Get(genre.Id, default)
            .Returns(genre);

        // Act
        var result = await _sut.RemoveGenreFromMovie(movie.Id, genre.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Update_WhenMovieIdisValid_ShouldReturnSuccess()
    {
        // Arrange
        var movie = _moviesContext[0];
        var updateMovieDTO = new UpdateMovieDTO
        {
            Title = "New Title",
            Synopsis = "New Synopsis",
        };

        _movieRepository.Get(movie.Id, default)
            .Returns(movie);
        _mapper.Map(updateMovieDTO, movie)
            .Returns(_trueMapper.Map(updateMovieDTO, movie));

        // Act
        var result = await _sut.Update(movie.Id, updateMovieDTO, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Update_WhenMovieIdIsInvalid_ShouldReturnBadRequestError()
    {
        // Arrange
        var updateMovieDTO = new UpdateMovieDTO
        {
            Title = "New Title",
            Synopsis = "New Synopsis",
        };

        _movieRepository.Get(4, default)
            .Returns((Movie?)null);

        // Act
        var result = await _sut.Update(4, updateMovieDTO, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("Invalid movie ID", result.Errors.First().Message);
    }

    [Fact]
    public async void Remove_WhenMovieIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var movie = _moviesContext[0];

        _movieRepository.Get(movie.Id, default)
            .Returns(movie);

        // Act
        var result = await _sut.Remove(movie.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Remove_WhenMovieIdIsInvalid_ShouldReturnBadRequestError()
    {
        // Arrange
        _movieRepository.Get(4, default)
            .Returns((Movie?)null);

        // Act
        var result = await _sut.Remove(4, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("Invalid movie ID", result.Errors.First().Message);
    }

    [Fact]
    public async void AddReviewToMovie_WhenMovieIdAndUserIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var movie = _moviesContext[0];
        var user = new User { Id = 4, Username = "User4" };
        var review = new CreateVoteDTO
        {
            Rating = 4
        };

        _movieRepository.Get(movie.Id, default)
            .Returns(movie);
        _userRepository.Get(user.Id, default)
            .Returns(user);
        _mapper.Map<Vote>(review)
            .Returns(_trueMapper.Map<Vote>(review));

        // Act
        var result = await _sut.AddReviewToMovie(movie.Id, user.Id, review, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(4, movie.Quantity_Votes);
    }

    [Fact]
    public async void AddReviewToMovie_WhenMovieIdIsInvalid_ShouldReturnBadRequestError()
    {
        // Arrange
        var user = new User { Id = 4, Username = "User4" };
        var review = new CreateVoteDTO { Rating = 4 };

        _movieRepository.Get(4, default)
            .Returns((Movie?)null);
        _userRepository.Get(user.Id, default)
            .Returns(user);

        // Act
        var result = await _sut.AddReviewToMovie(4, user.Id, review, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.IsType<BadRequestError>(result.Errors.First());
        Assert.Equal("Invalid movie or user ID", result.Errors.First().Message);
    }

    [Fact]
    public async void RemoveReviewFromMovie_WhenMovieIdisValid_ShouldReturnSuccess()
    {
        // Arrange
        var movie = _moviesContext[0];
        var user = new User { Id = 1, Username = "User1" };


        _voteRepository.GetComposite(movie.Id, user.Id, default)
            .Returns(new Vote
            {
                MovieId = movie.Id,
                Movie = movie,
                UserId = user.Id,
                User = user,
                Rating = 4
            });

        // Act
        var result = await _sut.RemoveReviewFromMovie(movie.Id, user.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, movie.Quantity_Votes);
    }


}
using AutoMapper;
using Domain.AutomapperProfiles;
using Domain.DTOs.ParticipantDTOs;
using Domain.Models;
using NSubstitute;
using Repository.Repositories.Abstract;
using Service.Services;
using Service.Utils.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services;

public class ParticipantServiceTesting
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IMapper _mapper;
    private readonly ParticipantService _sut;
    private readonly List<Participant> _context;

    public ParticipantServiceTesting()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ParticipantProfile());
        });

        _participantRepository = Substitute.For<IParticipantRepository>();
        _mapper = Substitute.For<IMapper>();

        _sut = new ParticipantService(_mapper, _participantRepository);

        _context = new List<Participant>
        {
            new Participant { Id = 1, Name = "Gamma", Biography = "Biography" },
            new Participant { Id = 2, Name = "Alpha", Biography = "Biography" },
            new Participant { Id = 3, Name = "Beta", Biography = "Biography" }
        };
    }

    [Fact]
    public void GetAll_WhenCalled_ReturnsAllParticipants()
    {
        // Arrange
        _participantRepository.GetAll()
            .Returns(_context);
        _mapper.Map<IEnumerable<ReadParticipantReferencelessDTO>>(_context)
            .Returns(new List<ReadParticipantReferencelessDTO>
            {
                new ReadParticipantReferencelessDTO { Id = 1, Name = "Gamma"},
                new ReadParticipantReferencelessDTO { Id = 2, Name = "Alpha"},
                new ReadParticipantReferencelessDTO { Id = 3, Name = "Beta"}
            });

        // Act
        var result = _sut.GetAll();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async void GetById_WhenIdIsValid_ReturnsParticipant()
    {
        // Arrange
        _participantRepository.Get(1, default)
            .Returns(_context[0]);
        _mapper.Map<ReadParticipantDTO>(_context[0])
            .Returns(new ReadParticipantDTO { Id = 1, Name = "Gamma" });

        // Act
        var result = await _sut.Get(1, CancellationToken.None);

        // Assert
        Assert.Equal(1, result!.Id);
        Assert.Equal("Gamma", result.Name);
    }

    [Fact]
    public void GetParticipantsByName_WhenCalled_ReturnsParticipantsWithMatchingNames()
    {
        // Arrange

        var context = new List<Participant>
        {
            new Participant { Id = 3, Name = "Beta", Biography = "Biography" }
        };

        _participantRepository.GetParticipantsByName("Beta")
            .Returns(context);
        _mapper.Map<IEnumerable<ReadParticipantReferencelessDTO>>(context)
            .Returns(new List<ReadParticipantReferencelessDTO>
            {
                new ReadParticipantReferencelessDTO { Id = 3, Name = "Beta"},
            });

        // Act
        var result = _sut.GetParticipantsByName("Beta", new CancellationToken());

        // Assert
        Assert.Equal(1, result.Count());
        Assert.Equal("Beta", result.First().Name);
    }

    [Fact]
    public async void Insert_WhenNameIsUnique_ShouldReturnSuccess()
    {
        // Arrange

        var newParticipant = new CreateParticipantDTO { Name = "Delta", Biography = "Biography" };

        _participantRepository.Insert(Arg.Any<Participant>(), CancellationToken.None)
            .Returns(new Participant { Id = 1, Name = "Delta", Biography = "Biography" });
        _mapper.Map<ReadParticipantReferencelessDTO>(newParticipant)
            .Returns(new ReadParticipantReferencelessDTO { Id = 1, Name = "Delta" });

        // Act
        var result = await _sut.Insert(newParticipant, new CancellationToken());

        // Assert
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public async void Insert_WhenNameIsNotUnique_ShouldReturnBadRequestError()
    {
        // Arrange
        var newParticipant = new CreateParticipantDTO { Name = "Beta", Biography = "Biography" };

        _participantRepository.GetParticipantsByName("Beta")
            .Returns(new List<Participant> { new Participant { Id = 3, Name = "Beta", Biography = "Biography" } });

        // Act
        var result = await _sut.Insert(newParticipant, new CancellationToken());

        // Assert
        Assert.IsType<BadRequestError>(result.Errors.First());
    }

    [Fact]
    public async void Update_WhenIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var updatedParticipant = new UpdateParticipantDTO {Name = "Delta", Biography = "Biography" };

        _participantRepository.Get(1, CancellationToken.None)
            .Returns(new Participant { Id = 1, Name = "Beta", Biography = "Biography" });

        _mapper.Map<ReadParticipantReferencelessDTO>(updatedParticipant)
            .Returns(new ReadParticipantReferencelessDTO { Id = 1, Name = "Delta" });

        // Act
        var result = await _sut.Update(1, updatedParticipant, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Update_WhenIdIsInvalid_ShouldReturnNotFoundError()
    {
        // Arrange
        var updatedParticipant = new UpdateParticipantDTO { Name = "Delta", Biography = "Biography" };

        _participantRepository.Get(1, CancellationToken.None)
            .Returns((Participant?)null);

        // Act
        var result = await _sut.Update(1, updatedParticipant, new CancellationToken());

        // Assert
        Assert.IsType<NotFoundError>(result.Errors.First());
    }

    [Fact]
    public async void Delete_WhenIdIsValid_ShouldReturnSuccess()
    {
        // Arrange
        _participantRepository.Get(1, CancellationToken.None)
            .Returns(new Participant { Id = 1, Name = "Gamma", Biography = "Biography" });

        // Act
        var result = await _sut.Remove(1, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void Delete_WhenIdIsInvalid_ShouldReturnNotFoundError()
    {
        // Arrange
        _participantRepository.Get(1, CancellationToken.None)
            .Returns((Participant?)null);

        // Act
        var result = await _sut.Remove(1, new CancellationToken());

        // Assert
        Assert.IsType<NotFoundError>(result.Errors.First());
    }
}

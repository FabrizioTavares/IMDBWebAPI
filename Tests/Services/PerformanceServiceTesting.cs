﻿using AutoMapper;
using Domain.AutomapperProfiles;
using Domain.DTOs.PerformanceDTOs;
using Domain.Models;

namespace Tests.Services;

public class PerformanceServiceTesting
{

    public readonly IMapper _mapper;

    public PerformanceServiceTesting()
    {

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new PerformanceProfile());
        });
        _mapper = config.CreateMapper();

    }

    [Fact]
    public void test()
    {
        var newPerformance = new CreatePerformanceDTO
        {
            ParticipantId = 1,
            //MovieId = 1
        };

        var performance = _mapper.Map<Performance>(newPerformance);
        Assert.Equal(1, performance.MovieId);
        Assert.Equal(1, performance.ParticipantId);
    }
}

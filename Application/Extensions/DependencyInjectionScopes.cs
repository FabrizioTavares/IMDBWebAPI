using FluentValidation;
using Repository.Repositories.Abstract;
using Repository.Repositories;
using Service.Services.Abstract;
using Service.Services;
using Service.Validation.Genre;
using Domain.DTOs.GenreDTOs;
using Domain.AutomapperProfiles;
using Domain.DTOs.ParticipantDTOs;
using Service.Validation.Participant;

namespace Application.Extensions
{
    public static class DependencyInjectionScopes
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IValidator<CreateGenreDTO>, CreateGenreDTOValidator>();
            services.AddScoped<IValidator<UpdateGenreDTO>, UpdateGenreDTOValidator>();

            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IValidator<CreateParticipantDTO>, CreateParticipantDTOValidator>();
            services.AddScoped<IValidator<UpdateParticipantDTO>, UpdateParticipantDTOValidator>();

            services.AddAutoMapper(
                typeof(GenreProfile),
                typeof(ParticipantProfile),
                typeof(PerformanceProfile),
                typeof(DirectionProfile));
            
            return services;
        }

    }
}

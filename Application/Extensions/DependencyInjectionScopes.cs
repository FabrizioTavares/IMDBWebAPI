using Domain.AutomapperProfiles;
using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.GenreDTOs;
using Domain.DTOs.MovieDTOs;
using Domain.DTOs.ParticipantDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.DTOs.UserDTOs;
using Domain.Utils.Cryptography;
using FluentValidation;
using Repository.Repositories;
using Repository.Repositories.Abstract;
using Service.Services;
using Service.Services.Abstract;
using Service.Validation.Direction;
using Service.Validation.Genre;
using Service.Validation.Movie;
using Service.Validation.Participant;
using Service.Validation.Performance;
using Service.Validation.User;

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

            services.AddScoped<IPerformanceRepository, PerformanceRepository>();
            services.AddScoped<IValidator<CreatePerformanceDTO>, CreatePerformanceDTOValidator>();
            services.AddScoped<IValidator<UpdatePerformanceDTO>, UpdatePerformanceDTOValidator>();

            services.AddScoped<IDirectionRepository, DirectionRepository>();
            services.AddScoped<IValidator<CreateDirectionDTO>, CreateDirectionDTOValidator>();

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IValidator<CreateMovieDTO>, CreateMovieDTOValidator>();
            services.AddScoped<IValidator<UpdateMovieDTO>, UpdateMovieDTOValidator>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IValidator<CreateUserDTO>, CreateUserDTOValidator>();

            services.AddSingleton<ICryptographer, SHA256Cryptographer>();

            services.AddAutoMapper(
                typeof(GenreProfile),
                typeof(ParticipantProfile),
                typeof(PerformanceProfile),
                typeof(DirectionProfile),
                typeof(MovieProfile),
                typeof(UserProfile),
                typeof(AdminProfile),
                typeof(VoteProfile)
                );

            return services;
        }

    }
}

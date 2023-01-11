using Domain.Utils.Cryptography;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Repository.Abstraction;
using Repository.AdminRepository;
using Repository.DirectionRepository;
using Repository.GenreRepository;
using Repository.MovieRepository;
using Repository.ParticipantRepository;
using Repository.PerformanceRepository;
using Repository.UserRepository;
using Repository.VoteRepository;
using Service;
using Service.Abstraction.AdministrativeServiceAbstractions;
using Service.Abstraction.AdministrativeServiceAbstractions.AdminDTOs;
using Service.Abstraction.AdministrativeServiceAbstractions.AdminValidation;
using Service.Abstraction.AuthenticableEntityAbstractions;
using Service.Abstraction.GenreServiceAbstractions;
using Service.Abstraction.GenreServiceAbstractions.GenreDTOs;
using Service.Abstraction.GenreServiceAbstractions.GenreValidation;
using Service.Abstraction.MovieServiceAbstractions;
using Service.Abstraction.MovieServiceAbstractions.DirectionDTOs;
using Service.Abstraction.MovieServiceAbstractions.DirectionValidation;
using Service.Abstraction.MovieServiceAbstractions.MovieDTOs;
using Service.Abstraction.MovieServiceAbstractions.MovieValidation;
using Service.Abstraction.MovieServiceAbstractions.PerformanceDTOs;
using Service.Abstraction.MovieServiceAbstractions.PerformanceValidation;
using Service.Abstraction.MovieServiceAbstractions.VoteDTOs;
using Service.Abstraction.MovieServiceAbstractions.VoteValidation;
using Service.Abstraction.ParticipantServiceAbstractions;
using Service.Abstraction.ParticipantServiceAbstractions.ParticipantDTOs;
using Service.Abstraction.ParticipantServiceAbstractions.ParticipantValidation;
using Service.Abstraction.UserServiceAbstractions;
using Service.Abstraction.UserServiceAbstractions.UserDTOs;
using Service.Abstraction.UserServiceAbstractions.UserValidation;
using System.Text;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
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

        services.AddScoped<IVoteRepository, VoteRepository>();
        services.AddScoped<IValidator<CreateVoteDTO>, CreateVoteDTOValidator>();

        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IValidator<CreateMovieDTO>, CreateMovieDTOValidator>();
        services.AddScoped<IValidator<UpdateMovieDTO>, UpdateMovieDTOValidator>();

        services.AddScoped<IAdministrativeService, AdministrativeService>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IValidator<CreateAdminDTO>, CreateAdminDTOValidator>();
        services.AddScoped<IValidator<UpdateAdminDTO>, UpdateAdminDTOValidator>();

        services.AddScoped<IAuthenticableEntityService, AuthenticationService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IValidator<CreateUserDTO>, CreateUserDTOValidator>();
        services.AddScoped<IValidator<UpdateUserDTO>, UpdateUserDTOValidator>();

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

    public static IServiceCollection AddJwtAuthenticationAndSwagger(this IServiceCollection services)
    {

        var _secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.ImdbApiSecret));
        var _scheme = JwtBearerDefaults.AuthenticationScheme;

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = _scheme;
            x.DefaultChallengeScheme = _scheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _secret,
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddSwaggerGen(
            c =>
            {
                c.AddSecurityDefinition(
                    "token",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer",
                        In = ParameterLocation.Header,
                        Name = HeaderNames.Authorization
                    }
                );
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
                    }
                );
            }
        );

        return services;
    }

}
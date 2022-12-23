using Domain.AutomapperProfiles;
using Domain.DTOs.DirectionDTOs;
using Domain.DTOs.GenreDTOs;
using Domain.DTOs.MovieDTOs;
using Domain.DTOs.ParticipantDTOs;
using Domain.DTOs.PerformanceDTOs;
using Domain.DTOs.UserDTOs;
using Domain.DTOs.VoteDTOs;
using Domain.Utils.Cryptography;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Repository.Repositories;
using Repository.Repositories.Abstract;
using Service;
using Service.Services;
using Service.Services.Abstract;
using Service.Validation.Direction;
using Service.Validation.Genre;
using Service.Validation.Movie;
using Service.Validation.Participant;
using Service.Validation.Performance;
using Service.Validation.User;
using Service.Validation.Vote;
using System.Text;

namespace Application.Extensions
{
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
            //TODO: Validation

            services.AddScoped<IAuthenticableEntityService, AuthenticationService>();

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

        public static IServiceCollection AddJwtAuthenticationAndSwagger(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.ImdbApiSecret)),
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

    
}

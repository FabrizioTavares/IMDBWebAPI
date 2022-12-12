using FluentValidation;
using Repository.Repositories.Abstract;
using Repository.Repositories;
using Service.Services.Abstraction;
using Service.Services;
using Service.Validation.Genre;
using Domain.DTOs.GenreDTOs;
using Domain.AutomapperProfiles;

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
            services.AddAutoMapper(typeof(GenreProfile));

            return services;
        }

    }
}

using AutoMapper;
using Domain.DTOs.GenreDTOs;
using Domain.Models;
using Repository.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Abstraction
{
    public interface IGenreService
    {
        IEnumerable<ReadGenreDTO?> GetAll();
        Task<ReadGenreDTO?> Get(int id, CancellationToken cancellationToken);
        IEnumerable<ReadGenreDTO?> GetGenresByTitle(string title, CancellationToken cancellationToken);
        Task Insert(CreateGenreDTO genre, CancellationToken cancellationToken);
        Task Update(int id, UpdateGenreDTO genre, CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
    }
}

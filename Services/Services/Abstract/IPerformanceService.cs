using Domain.DTOs.PerformanceDTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Abstract
{
    public interface IPerformanceService
    {
        Task<Performance> Insert(CreatePerformanceDTO performanceDTO);
        Task<Performance> Update(UpdatePerformanceDTO performanceDTO);
        Task<Performance> Delete(int id);
        Task<Performance?> Get(int id);
        Task<Performance> GetByCharacterName(string characterName);
        IEnumerable<Performance?> GetAll();
    }
}

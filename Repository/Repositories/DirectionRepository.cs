using Domain.Models;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    internal class DirectionRepository : BaseRepository<Direction>, IDirectionRepository
    {
        public DirectionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
    }
}

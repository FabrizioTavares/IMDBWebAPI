using Domain.Models;
using Domain.Utils.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Repository.Seeding
{
    public class InitialAdministrator
    {
        private readonly ModelBuilder _builder;
        private readonly ICryptographer _cryptographer;

        public InitialAdministrator(ModelBuilder modelBuilder)
        {
            _builder = modelBuilder;
            _cryptographer = new SHA256Cryptographer();
        }

        public void Seed()
        {
            var salt = _cryptographer.GenerateSalt();
            _builder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Username = "admin",
                Password = _cryptographer.Hash("admin", salt),
                Salt = salt,
                Hierarchy = 100
            });
        }
    }
}

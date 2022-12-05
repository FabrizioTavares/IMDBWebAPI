﻿using Domain.Models;
using Domain.Utils.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Repository.Seeding
{
    internal class InitialAdministrator
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
            _builder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Username = "admin",
                Password = _cryptographer.Hash("admin"),
                Hierarchy = 100
            });
        }
    }
}

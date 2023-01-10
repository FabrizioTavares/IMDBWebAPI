using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.AdminRepository;
using Repository.DirectionRepository;
using Repository.GenreRepository;
using Repository.MovieRepository;
using Repository.ParticipantRepository;
using Repository.PerformanceRepository;
using Repository.Seeding;
using Repository.UserRepository;
using Repository.VoteRepository;

namespace Repository.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Direction> Directions { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Performance> Performances { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vote> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AdminMap());
        builder.ApplyConfiguration(new DirectionMap());
        builder.ApplyConfiguration(new GenreMap());
        builder.ApplyConfiguration(new MovieMap());
        builder.ApplyConfiguration(new ParticipantMap());
        builder.ApplyConfiguration(new PerformanceMap());
        builder.ApplyConfiguration(new UserMap());
        builder.ApplyConfiguration(new VoteMap());

        new InitialAdministrator(builder).Seed();
    }

}
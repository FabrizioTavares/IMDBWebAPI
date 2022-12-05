using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Maps
{
    internal class MovieMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder
                .HasKey(m => m.Id);
            builder
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(m => m.Synopsis)
                .IsRequired(false)
                .HasMaxLength(1000);
            builder
                .Property(m => m.ReleaseYear)
                .IsRequired(false);
            builder.Property(m => m.Duration)
                .IsRequired();
            builder
                .HasMany(m => m.Direction)
                .WithOne(d => d.Movie)
                .HasForeignKey(m => m.MovieId);
            builder
                .HasMany(m => m.Cast)
                .WithOne(p => p.Movie)
                .HasForeignKey(p => p.MovieId);
            builder
                .HasMany(m => m.Genres)
                .WithMany(g => g.Movies);
            builder
                .HasMany(m => m.Votes)
                .WithOne(r => r.Movie);
            builder.Navigation(m => m.Cast).AutoInclude(false);
        }
    }
}

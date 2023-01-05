using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Maps;

public class GenreMap : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder
            .HasKey(g => g.Id);
        builder
            .Property(g => g.Title)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .HasMany(g => g.Movies)
            .WithMany(m => m.Genres);
    }
}
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.PerformanceRepository;

public class PerformanceMap : IEntityTypeConfiguration<Performance>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Performance> builder)
    {
        builder.HasKey(a => new { a.MovieId, a.ParticipantId });
        builder.HasOne(a => a.Movie).WithMany(m => m.Cast).HasForeignKey(a => a.MovieId);
        builder.HasOne(a => a.Participant).WithMany(p => p.MoviesActedIn).HasForeignKey(a => a.ParticipantId);
        builder.Property(a => a.CharacterName).IsRequired(false).HasMaxLength(100);
    }
}
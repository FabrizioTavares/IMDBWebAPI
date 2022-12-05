using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public class DirectionMap : IEntityTypeConfiguration<Direction>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Direction> builder)
        {
            builder.HasKey(d => new { d.MovieId, d.ParticipantId });
            builder.HasOne(d => d.Movie).WithMany(m => m.Direction).HasForeignKey(a => a.MovieId);
            builder.HasOne(d => d.Participant).WithMany(p => p.MoviesDirected).HasForeignKey(a => a.ParticipantId);
        }
    }
}

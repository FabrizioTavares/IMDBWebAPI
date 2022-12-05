using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Maps
{
    public class ParticipantMap : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(p => p.Id);
            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(p => p.Biography)
                .IsRequired(false)
                .HasMaxLength(1000);
            builder
                .HasMany(p => p.MoviesActedIn)
                .WithOne(p => p.Participant)
                .HasForeignKey(p => p.ParticipantId)
                .IsRequired(false);
            builder
                .HasMany(p => p.MoviesDirected)
                .WithOne(d => d.Participant)
                .HasForeignKey(d => d.ParticipantId)
                .IsRequired(false);
        }
    }
}

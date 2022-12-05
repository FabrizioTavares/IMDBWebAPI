using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);
            builder
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(u => u.IsActive)
                .IsRequired();
            builder
                .HasMany(u => u.Votes)
                .WithOne(v => v.Voter);
        }
    }
}

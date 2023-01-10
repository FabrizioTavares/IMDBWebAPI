using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.UserRepository;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(u => u.Id);
        builder
            .HasIndex(u => u.Username)
            .IsUnique();
        builder
            .Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(32);
        builder
            .Property(u => u.Salt)
            .IsRequired()
            .HasMaxLength(8);
        builder
            .Property(u => u.IsActive)
            .IsRequired();
        builder
            .HasMany(u => u.Votes)
            .WithOne(v => v.User);
    }
}
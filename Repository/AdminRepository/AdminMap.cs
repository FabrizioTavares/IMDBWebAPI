using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.AdminRepository;

public class AdminMap : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder
            .HasKey(a => a.Id);
        builder
            .HasIndex(a => a.Username)
            .IsUnique();
        builder
            .Property(a => a.Username)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .Property(a => a.Password)
            .IsRequired()
            .HasMaxLength(32);
        builder
            .Property(a => a.Salt)
            .IsRequired()
            .HasMaxLength(8);
        builder
            .Property(a => a.IsActive)
            .IsRequired();
    }
}
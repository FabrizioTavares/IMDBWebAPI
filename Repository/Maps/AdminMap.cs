using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Maps
{
    public class AdminMap : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder
                .HasKey(a => a.Id);
            builder
                .Property(a => a.Username)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(a => a.Password)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(a => a.IsActive)
                .IsRequired();
        }
    }
}

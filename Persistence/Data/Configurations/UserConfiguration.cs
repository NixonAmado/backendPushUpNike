
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.Property("Name")
        .IsRequired()
        .HasColumnName("user_name")
        .HasMaxLength(40);

        builder.Property("Email")
        .IsRequired()
        .HasColumnName("email")
        .HasMaxLength(40);

        builder.Property("Password")
        .IsRequired()
        .HasColumnName("password")
        .HasMaxLength(255);
        

    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class StatusConfiguration:IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("status");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Description).HasMaxLength(45);
    }
}
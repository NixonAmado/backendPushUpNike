
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class SizeConfiguration:IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("size");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Description).HasMaxLength(45);
    }
}
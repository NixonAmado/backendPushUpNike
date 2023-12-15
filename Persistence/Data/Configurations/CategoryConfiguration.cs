
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class CategoryConfiguration:IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("category");

            builder.Property(e => e.Description).HasMaxLength(100);
    }
}
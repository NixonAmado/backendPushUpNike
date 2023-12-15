
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class SizeHasProductConfiguration:IEntityTypeConfiguration<SizeHasProduct>
{
    public void Configure(EntityTypeBuilder<SizeHasProduct> builder)
    {
        builder.HasKey(e => new { e.SizeId, e.ProductId })
            .HasName("PRIMARY")
            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        builder.ToTable("size_has_product");

        builder.HasIndex(e => e.ProductId, "fk_Size_has_Product_Product1_idx");

        builder.HasIndex(e => e.SizeId, "fk_Size_has_Product_Size_idx");

        builder.Property(e => e.SizeId).HasColumnName("Size_id");
        builder.Property(e => e.ProductId).HasColumnName("Product_id");
        builder.Property(e => e.Quantity).HasColumnName("quantity");

        builder.HasOne(d => d.Product).WithMany(p => p.SizeHasProducts)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_Size_has_Product_Product1");

        builder.HasOne(d => d.Size).WithMany(p => p.SizeHasProducts)
            .HasForeignKey(d => d.SizeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_Size_has_Product_Size");
    }
}
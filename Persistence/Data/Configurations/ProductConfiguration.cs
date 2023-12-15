
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class ProductConfiguration:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("product");

        builder.HasIndex(e => e.CategoryId, "fk_Product_Category1_idx");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.CategoryId).HasColumnName("Category_Id");
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.Image).HasMaxLength(255);
        builder.Property(e => e.PurchasePrice)
            .HasPrecision(10, 2)
            .HasColumnName("Purchase_price");
        builder.Property(e => e.SalePrice)
            .HasPrecision(10, 2)
            .HasColumnName("Sale_price");
        builder.Property(e => e.Title).HasMaxLength(150);

        builder.HasOne(d => d.Category).WithMany(p => p.Products)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_Product_Category1");
    }
}
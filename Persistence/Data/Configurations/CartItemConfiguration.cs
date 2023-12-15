
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class CartItemConfiguration:IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
            builder.HasKey(e => new { e.CartId, e.ProductId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            builder.ToTable("cart_item");

            builder.HasIndex(e => e.ProductId, "fk_cart_has_Product_Product1_idx");

            builder.HasIndex(e => e.CartId, "fk_cart_has_Product_cart1_idx");

            builder.Property(e => e.CartId).HasColumnName("Cart_id");
            builder.Property(e => e.ProductId).HasColumnName("Product_id");

            builder.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cart_has_Product_cart1");

            builder.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cart_has_Product_Product1");
    }
}
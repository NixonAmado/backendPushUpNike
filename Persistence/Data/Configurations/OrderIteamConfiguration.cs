
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class OrderItemConfiguration:IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
            builder.HasKey(e => new { e.OrderId, e.ProductId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            builder.ToTable("order_item");

            builder.HasIndex(e => e.OrderId, "fk_Order_has_Product_Order1_idx");

            builder.HasIndex(e => e.ProductId, "fk_Order_has_Product_Product1_idx");

            builder.Property(e => e.OrderId).HasColumnName("Order_Id");
            builder.Property(e => e.ProductId).HasColumnName("Product_id");
            builder.Property(e => e.Price).HasPrecision(10, 2);

            builder.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_has_Product_Order1");

            builder.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_has_Product_Product1");
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class OrderConfiguration:IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("order");

            builder.HasIndex(e => e.PaymentId, "fk_Order_Payment1_idx");

            builder.HasIndex(e => e.StatusId, "fk_Order_Status1_idx");

            builder.Property(e => e.Address).HasMaxLength(45);
            builder.Property(e => e.PaymentId).HasColumnName("Payment_Id");
            builder.Property(e => e.StatusId).HasColumnName("Status_id");

            builder.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_Payment1");

            builder.HasOne(d => d.UserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.User_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_User1");

            builder.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_Status1");
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class PaymentConfiguration:IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("payment");

            builder.HasIndex(e => e.PaymentMethodId, "fk_Payment_Payment_method1_idx");

            builder.Property(e => e.PaymentDate).HasColumnName("Payment_date");
            builder.Property(e => e.PaymentMethodId).HasColumnName("Payment_method_Id");

            builder.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Payment_Payment_method1");
    }
}
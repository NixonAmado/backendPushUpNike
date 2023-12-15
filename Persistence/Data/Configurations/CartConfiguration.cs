
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class CartConfiguration:IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.ToTable("cart");
            
            builder.HasOne(d => d.UserNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.User_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Cart_user1");            
    }
}
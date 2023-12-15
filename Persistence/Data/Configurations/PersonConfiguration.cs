
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class PersonConfiguration:IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("person");

            builder.HasIndex(e => e.UserId, "fk_Person_user1_idx");

            builder.Property(e => e.LastName).HasMaxLength(45);
            builder.Property(e => e.Name).HasMaxLength(45);
            builder.Property(e => e.UserId).HasColumnName("user_Id");

            builder.HasOne(d => d.User).WithMany(p => p.People)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Person_user1");
    }
}
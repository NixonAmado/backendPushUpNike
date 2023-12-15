using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data;

public partial class DbAppContext : DbContext
{
    public DbAppContext()
    {
    }

    public DbAppContext(DbContextOptions<DbAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RefreshToken> Refreshtokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<SizeHasProduct> SizeHasProducts { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
 
        OnModelCreatingPartial(modelBuilder);
         base.OnModelCreating(modelBuilder);
        var roles = new[]
        {
            new Role { Id = 1, Description = "Administrador" },
            new Role { Id = 2, Description = "Empleado" },
            // Agrega otros roles según tus necesidades
        };

        modelBuilder.Entity<Role>().HasData(roles);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

namespace Domain.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal PurchasePrice { get; set; }

    public decimal SalePrice { get; set; }

    public string Image { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<SizeHasProduct> SizeHasProducts { get; set; } = new List<SizeHasProduct>();
}

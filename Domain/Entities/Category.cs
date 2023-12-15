namespace Domain.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

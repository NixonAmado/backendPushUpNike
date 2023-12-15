namespace Domain.Entities;

public partial class SizeHasProduct
{
    public int SizeId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}

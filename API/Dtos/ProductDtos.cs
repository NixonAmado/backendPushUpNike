namespace API.Dtos;

public partial class G_ProductDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal PurchasePrice { get; set; }

    public decimal SalePrice { get; set; }

    public string Image { get; set; } = null!;

    public int CategoryId { get; set; }

}

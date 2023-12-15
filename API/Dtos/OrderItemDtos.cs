namespace API.Dtos;

public partial class G_OrderItemDto
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

}

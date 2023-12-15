namespace API.Dtos;

public partial class G_OrderDto
{
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public int Price { get; set; }
    public int PaymentId { get; set; }
    public int User_Id {get;set;}
}

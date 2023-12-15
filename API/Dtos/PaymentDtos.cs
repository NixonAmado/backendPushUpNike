namespace API.Dtos;

public partial class G_PaymentDto
{
    public int Id { get; set; }

    public DateOnly PaymentDate { get; set; }

    public int PaymentMethodId { get; set; }
}

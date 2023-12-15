namespace Domain.Entities;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

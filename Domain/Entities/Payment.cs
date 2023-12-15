namespace Domain.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public DateOnly PaymentDate { get; set; }

    public int PaymentMethodId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}

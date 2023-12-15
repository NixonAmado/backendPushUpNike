namespace Domain.Entities;

public partial class Order
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public int Price { get; set; }

    public int StatusId { get; set; }

    public int PaymentId { get; set; }
    public int User_Id {get;set;}
    public User UserNavigation {get;set;}


    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Payment Payment { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}

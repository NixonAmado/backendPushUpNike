namespace Domain.Entities;
public partial class Size
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<SizeHasProduct> SizeHasProducts { get; set; } = new List<SizeHasProduct>();
}

namespace Domain.Entities;

public partial class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
